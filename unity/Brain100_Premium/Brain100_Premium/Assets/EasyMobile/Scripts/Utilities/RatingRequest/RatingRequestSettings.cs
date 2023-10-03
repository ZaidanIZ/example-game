using UnityEngine;
using System.Collections;

namespace EasyMobile
{
    [System.Serializable]
    public class RatingRequestSettings
    {
        // Placeholders for replacable strings.
        public const string PRODUCT_NAME_PLACEHOLDER = "$PRODUCT_NAME";

        public iOSRatingDialog IosRatingDialogContent { get { return _iosDialogContent; } }

        public AndroidRatingDialog AndroidRatingDialogContent { get { return _androidDialogContent; } }

        public uint MinimumAcceptedStars { get { return _minimumAcceptedStars; } }

        public string SupportEmail { get { return _supportEmail; } }

        public string IosAppId { get { return _iosAppId; } }

        public uint AnnualCap { get { return _annualCap; } }

        // Appearance
        [SerializeField]
        private iOSRatingDialog _iosDialogContent = new iOSRatingDialog();
        [SerializeField]
        private AndroidRatingDialog _androidDialogContent = new AndroidRatingDialog();

        // Behaviour
        [SerializeField][Range(0, 5)]
        private uint _minimumAcceptedStars = 4;
        [SerializeField]
        private string _supportEmail;
        [SerializeField]
        private string _iosAppId;
        [SerializeField][Range(3, 100)]
        private uint _annualCap = 12;

        [System.Serializable]
        public class AndroidRatingDialog
        {
            public string title = "Rate " + PRODUCT_NAME_PLACEHOLDER;
            public string startMessage = "How would you rate " + PRODUCT_NAME_PLACEHOLDER + "?";
            public string lowRatingMessage = "That's bad. Would you like to give us some feedback instead?";
            public string highRatingMessage = "Awesome! Let's do it!";
            public string postponeButtonText = "Not Now";
            public string refuseButtonText = "Don't Ask Again";
            public string cancelButtonText = "Cancel";
            public string feedbackButtonText = "Send Feedback";
            public string rateButtonText = "Rate Now!";
        }

        [System.Serializable]
        public class iOSRatingDialog
        {
            // For iOS pre-10.3 only
            public string title = "Rate " + PRODUCT_NAME_PLACEHOLDER;
            public string message = "Would you mind spending a moment to rate " + PRODUCT_NAME_PLACEHOLDER + " on the App Store?";
            public string postponeButtonText = "Remind Me Later";
            public string refuseButtonText = "Don't Ask Again";
            public string rateButtonText = "Rate Now!";
        }
    }
}

