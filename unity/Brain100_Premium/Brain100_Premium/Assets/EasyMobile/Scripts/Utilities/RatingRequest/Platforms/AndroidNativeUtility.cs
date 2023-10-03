using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_ANDROID
namespace EasyMobile
{
    internal static class AndroidNativeUtility
    {
        private static readonly string ANDROID_JAVA_UTILITY_CLASS = "com.sglib.easymobile.androidnative.EMUtility";

        internal static void RequestRating(RatingRequestSettings settings)
        {
            var content = settings.AndroidRatingDialogContent;
            AndroidUtil.CallJavaStaticMethod(ANDROID_JAVA_UTILITY_CLASS, "RequestReview",
                content.title,
                content.startMessage,
                content.lowRatingMessage,
                content.highRatingMessage,
                content.postponeButtonText,
                content.refuseButtonText,
                content.cancelButtonText,
                content.feedbackButtonText,
                content.rateButtonText,
                settings.MinimumAcceptedStars.ToString()
            );
        }
    }
}
#endif