using UnityEngine;
using System.Collections;
using System;

namespace EasyMobile
{
    [AddComponentMenu("")]
    public class MobileNativeRatingRequest : MonoBehaviour
    {
        public enum UserAction
        {
            Refuse,
            Postpone,
            Feedback,
            Rate
        }

        #if UNITY_ANDROID
        // The current active rating request dialog
        public static MobileNativeRatingRequest Instance { get; private set; }
        #endif

        #if UNITY_ANDROID && !UNITY_EDITOR
        private static readonly string RATING_DIALOG_GAMEOBJECT = "MobileNativeRatingDialog";
        #endif

        // Remember to not show up again if the user refuses to give feedback.
        private const string RATING_REQUEST_DISABLE_PPKEY = "EM_RATING_REQUEST_DISABLE";
        private const int RATING_REQUEST_ENABLED = 1;
        private const int RATING_REQUEST_DISABLED = -1;
        private const string ANNUAL_REQUESTS_MADE_PPKEY_PREFIX = "EM_RATING_REQUESTS_MADE_YEAR_";

        private static Action<UserAction> customBehaviour;

        #region Public API

        /// <summary>
        /// Shows the rating request dialog with the default behaviour. 
        /// </summary>
        public static void RequestRating()
        {
            customBehaviour = null;
            DoRequestRating();
        }

        /// <summary>
        /// Shows the rating request dialog with a callback for custom behaviour.
        /// </summary>
        /// <param name="callback">Callback.</param>
        public static void RequestRating(Action<UserAction> callback)
        {
            customBehaviour = callback;
            DoRequestRating();
        }

        /// <summary>
        /// Determines if a rating request can be made, which means it was not previously disabled
        /// by the user, the user hasn't accepted to rate before, and the annual cap hasn't been met yet.
        /// </summary>
        /// <returns><c>true</c> if can request rating; otherwise, <c>false</c>.</returns>
        public static bool CanRequestRating()
        {
            return (GetThisYearRemainingRequests() > 0) && !IsRatingRequestDisabled();
        }

        /// <summary>
        /// Determines if the rating request dialog has been disabled.
        /// Disabling occurs if the user either selects the "refuse" button or the "rate" button.
        /// On iOS, this is only applicable to versions older than 10.3.
        /// </summary>
        /// <returns><c>true</c> if rating request is disabled; otherwise, <c>false</c>.</returns>
        public static bool IsRatingRequestDisabled()
        {
            return PlayerPrefs.GetInt(RATING_REQUEST_DISABLE_PPKEY, RATING_REQUEST_ENABLED) == RATING_REQUEST_DISABLED;
        }

        /// <summary>
        /// Gets the number of unused requests in this year.
        /// Note that this is not applicable to iOS 10.3 or newer.
        /// </summary>
        /// <returns>This year unused requests.</returns>
        public static int GetThisYearRemainingRequests()
        {
            int usedRequests = GetAnnualUsedRequests(DateTime.Now.Year);
            return (int)EM_Settings.RatingRequest.AnnualCap - usedRequests;
        }

        /// <summary>
        /// Disables the rating request dialog show that it can't be shown anymore.
        /// </summary>
        public static void DisableRatingRequest()
        {
            PlayerPrefs.SetInt(RATING_REQUEST_DISABLE_PPKEY, RATING_REQUEST_DISABLED);
            PlayerPrefs.Save();
        }

        #endregion

        #region Private methods

        private static int GetAnnualUsedRequests(int year)
        {
            string key = ANNUAL_REQUESTS_MADE_PPKEY_PREFIX + year.ToString();
            return PlayerPrefs.GetInt(key, 0);
        }

        private static void SetAnnualUsedRequests(int year, int requestNumber)
        {
            string key = ANNUAL_REQUESTS_MADE_PPKEY_PREFIX + year.ToString();
            PlayerPrefs.SetInt(key, requestNumber);
        }

        private static void DoRequestRating()
        {
            if (!CanRequestRating())
            {
                Debug.Log("Rating request was either disabled or the annual cap was reached.");
                return;
            }

            #if UNITY_EDITOR
            Debug.Log("Request review is only available on iOS and Android devices.");
            #elif UNITY_IOS
            if (iOSNativeUtility.CanUseBuiltinRequestReview())
            {
                // iOS 10.3+.
                iOSNativeUtility.RequestReview();
            }
            else
            {
                // iOS before 10.3.
                var content = EM_Settings.RatingRequest.IosRatingDialogContent;
                MobileNativeAlert alert = MobileNativeAlert.ShowThreeButtonAlert(
                                              content.title.Replace(RatingRequestSettings.PRODUCT_NAME_PLACEHOLDER, Application.productName),
                                              content.message.Replace(RatingRequestSettings.PRODUCT_NAME_PLACEHOLDER, Application.productName),
                                              content.refuseButtonText,
                                              content.postponeButtonText,
                                              content.rateButtonText
                                          );

                if (alert != null)
                {
                    alert.OnComplete += OnIosRatingDialogCallback;
                }

                // Increment the number of requests used this year.
                SetAnnualUsedRequests(DateTime.Now.Year, GetAnnualUsedRequests(DateTime.Now.Year) + 1);
            }
            #elif UNITY_ANDROID
            if (Instance != null)
                return;    // only allow one alert at a time

            // Create a Unity game object to receive messages from native side
            Instance = new GameObject(RATING_DIALOG_GAMEOBJECT).AddComponent<MobileNativeRatingRequest>();

            // Show the Android rating request
            var settings = EM_Settings.RatingRequest;
            var content = settings.AndroidRatingDialogContent;
            content.title = content.title.Replace(RatingRequestSettings.PRODUCT_NAME_PLACEHOLDER, Application.productName);
            content.startMessage = content.startMessage.Replace(RatingRequestSettings.PRODUCT_NAME_PLACEHOLDER, Application.productName);
            content.lowRatingMessage = content.lowRatingMessage.Replace(RatingRequestSettings.PRODUCT_NAME_PLACEHOLDER, Application.productName);
            content.highRatingMessage = content.highRatingMessage.Replace(RatingRequestSettings.PRODUCT_NAME_PLACEHOLDER, Application.productName);
            AndroidNativeUtility.RequestRating(settings);

            // Increment the number of requests used this year.
            SetAnnualUsedRequests(DateTime.Now.Year, GetAnnualUsedRequests(DateTime.Now.Year) + 1);
            #else
            Debug.Log("Request review is not supported on this platform.");
            #endif
        }

        private static void DefaultCallback(UserAction action)
        {
            if (customBehaviour != null)
                customBehaviour(action);
            else
                PerformDefaultBehaviour(action);
        }

        private static void PerformDefaultBehaviour(UserAction action)
        {
            switch (action)
            {
                case UserAction.Refuse:
                    // Never ask again.
                    DisableRatingRequest();
                    break;
                case UserAction.Postpone:
                    // Do nothing, the dialog simply closes.
                    break;
                case UserAction.Feedback:
                    // Open email client.
                    Application.OpenURL("mailto:" + EM_Settings.RatingRequest.SupportEmail);
                    break;
                case UserAction.Rate:
                    // Open store page.
                    if (Application.platform == RuntimePlatform.IPhonePlayer)
                    {
                        Application.OpenURL("itms-apps://itunes.apple.com/app/id" + EM_Settings.RatingRequest.IosAppId + "?action=write-review");
                    }
                    else if (Application.platform == RuntimePlatform.Android)
                    {
                        #if UNITY_5_6_OR_NEWER
                        Application.OpenURL("market://details?id=" + Application.identifier);
                        #else
                        Application.OpenURL("market://details?id=" + Application.bundleIdentifier);
                        #endif
                    }
                    // The user has rated, don't ask again.
                    DisableRatingRequest();
                    break;
            }
        }

        // Map button index into UserAction.
        private static UserAction ConvertToUserAction(int index)
        {
            #if UNITY_IOS
            // Applicable for pre-10.3 only. Note that there's no Feedback option.
            switch (index)
            {
                case 0:
                    return UserAction.Refuse;
                case 1:
                    return UserAction.Postpone;
                case 2:
                    return UserAction.Rate;
                default:
                    return UserAction.Postpone;
            }
            #elif UNITY_ANDROID
            switch (index)
            {
                case 0:
                    return UserAction.Refuse;
                case 1:
                    return UserAction.Postpone;
                case 2:
                    return UserAction.Feedback;
                case 3:
                    return UserAction.Rate;
                default:
                    return UserAction.Postpone;
            }
            #else
            return UserAction.Postpone;
            #endif
        }

        #if UNITY_IOS
        // Pre-10.3 iOS rating dialog callback
        private static void OnIosRatingDialogCallback(int button)
        {
            // Always go through the default callback first.
            DefaultCallback(ConvertToUserAction(button));
        }
        #endif

        #if UNITY_ANDROID
        // Callback to be called from Android native side with UnitySendMessage
        private void OnAndroidRatingDialogCallback(string userAction)
        {
            int index = Convert.ToInt16(userAction);

            // Always go through the default callback first.
            DefaultCallback(ConvertToUserAction(index));

            // Destroy the used object
            Instance = null;
            Destroy(gameObject);
        }
        #endif

        #endregion
    }
}

