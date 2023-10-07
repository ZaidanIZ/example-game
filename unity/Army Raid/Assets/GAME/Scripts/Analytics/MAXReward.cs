using System;
using UnityEngine;

public class MAXReward : MonoBehaviour
{
  private int _idRewardButton;

  // Level counter for launching Interstitial
  private static int AdsInterCounter;
  string adUnitIdREWARD = "YOUR_AD_UNIT_ID";
  string adUnitIdInterstitial = "YOUR_AD_UNIT_ID";
  int retryAttempt;

  private void Awake()
  {
    _idRewardButton = -1;
  }

  private void Init()
  {
    MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) =>
    {
      // AppLovin SDK is initialized, start loading ads
    };

    MaxSdk.SetSdkKey("YOUR_SDK_KEY");
    MaxSdk.InitializeSdk();
  }

  private void Start()
  {
    Init();
    InitializeRewardedAds();
    InitializeInterstitialAds();
    
    AdsInterCounter++;

    if (AdsInterCounter >= 2)
    {
      AdsInterCounter = 0;
      //ShowInterAdv;
      ShowInterstitial();
    }
  }

  //ADS BUTTON
  public void AdsGetReward(int _id)
  {
    _idRewardButton = _id;

    //ShowRewardAdv;
    if (MaxSdk.IsRewardedAdReady(adUnitIdREWARD))
    {
      MaxSdk.ShowRewardedAd(adUnitIdREWARD);
    }
  }

  // If the ad is successful, then the function
  private void GetReward()
  {
    switch (_idRewardButton)
    {
      case 1:
        ComponentsManager.UpgradeUI.UpgradeWarriors();
        break;

      case 2:
        ComponentsManager.UpgradeUI.UpgradeArchers();
        break;
    }
  }

  public void ShowInterstitial()
  {
    if (MaxSdk.IsInterstitialReady(adUnitIdInterstitial))
    {
      MaxSdk.ShowInterstitial(adUnitIdInterstitial);
    }
  }

  public void InitializeRewardedAds()
  {
    // Attach callback
    MaxSdkCallbacks.Rewarded.OnAdLoadedEvent += OnRewardedAdLoadedEvent;
    MaxSdkCallbacks.Rewarded.OnAdLoadFailedEvent += OnRewardedAdLoadFailedEvent;
    MaxSdkCallbacks.Rewarded.OnAdDisplayedEvent += OnRewardedAdDisplayedEvent;
    MaxSdkCallbacks.Rewarded.OnAdClickedEvent += OnRewardedAdClickedEvent;
    MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent += OnRewardedAdRevenuePaidEvent;
    MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += OnRewardedAdHiddenEvent;
    MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent += OnRewardedAdFailedToDisplayEvent;
    MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;

    // Load the first rewarded ad
    LoadRewardedAd();
  }

  private void LoadRewardedAd()
  {
    MaxSdk.LoadRewardedAd(adUnitIdREWARD);
  }

  private void OnRewardedAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
  {
    // Rewarded ad is ready for you to show. MaxSdk.IsRewardedAdReady(adUnitId) now returns 'true'.

    // Reset retry attempt
    retryAttempt = 0;
  }

  private void OnRewardedAdLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
  {
    // Rewarded ad failed to load 
    // AppLovin recommends that you retry with exponentially higher delays, up to a maximum delay (in this case 64 seconds).

    retryAttempt++;
    double retryDelay = Math.Pow(2, Math.Min(6, retryAttempt));

    Invoke("LoadRewardedAd", (float) retryDelay);
  }

  private void OnRewardedAdDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
  {
  }

  private void OnRewardedAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo,
    MaxSdkBase.AdInfo adInfo)
  {
    // Rewarded ad failed to display. AppLovin recommends that you load the next ad.
    LoadRewardedAd();
  }

  private void OnRewardedAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
  {
  }

  private void OnRewardedAdHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
  {
    // Rewarded ad is hidden. Pre-load the next ad
    LoadRewardedAd();
  }

  private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward, MaxSdkBase.AdInfo adInfo)
  {
    GetReward();
  }

  private void OnRewardedAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
  {
    // Ad revenue paid. Use this callback to track user revenue.
  }

  public void InitializeInterstitialAds()
  {
    // Attach callback
    MaxSdkCallbacks.Interstitial.OnAdLoadedEvent += OnInterstitialLoadedEvent;
    MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += OnInterstitialLoadFailedEvent;
    MaxSdkCallbacks.Interstitial.OnAdDisplayedEvent += OnInterstitialDisplayedEvent;
    MaxSdkCallbacks.Interstitial.OnAdClickedEvent += OnInterstitialClickedEvent;
    MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += OnInterstitialHiddenEvent;
    MaxSdkCallbacks.Interstitial.OnAdDisplayFailedEvent += OnInterstitialAdFailedToDisplayEvent;

    // Load the first interstitial
    LoadInterstitial();
  }

  private void LoadInterstitial()
  {
    MaxSdk.LoadInterstitial(adUnitIdInterstitial);
  }

  private void OnInterstitialLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
  {
    // Interstitial ad is ready for you to show. MaxSdk.IsInterstitialReady(adUnitId) now returns 'true'

    // Reset retry attempt
    retryAttempt = 0;
  }

  private void OnInterstitialLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
  {
    // Interstitial ad failed to load 
    // AppLovin recommends that you retry with exponentially higher delays, up to a maximum delay (in this case 64 seconds)

    retryAttempt++;
    double retryDelay = Math.Pow(2, Math.Min(6, retryAttempt));

    Invoke("LoadInterstitial", (float) retryDelay);
  }

  private void OnInterstitialDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
  {
  }

  private void OnInterstitialAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo,
    MaxSdkBase.AdInfo adInfo)
  {
    // Interstitial ad failed to display. AppLovin recommends that you load the next ad.
    LoadInterstitial();
  }

  private void OnInterstitialClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
  {
  }

  private void OnInterstitialHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
  {
    // Interstitial ad is hidden. Pre-load the next ad.
    LoadInterstitial();
  }
}