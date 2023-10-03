using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using EasyMobile;

namespace EasyMobile.Demo
{
    public class UtilitiesDemo : MonoBehaviour
    {
        public void RequestRating()
        {
            if (MobileNativeRatingRequest.CanRequestRating())
                MobileNativeRatingRequest.RequestRating();
            else
                MobileNativeUI.Alert("Alert", "The rating dialog was disabled (user selected Don't Ask Again or already opted to rate) or the annual cap was reached.");
        }
    }
}
