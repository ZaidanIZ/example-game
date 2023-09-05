using UnityEngine;
using System;
using UnityEngine.Advertisements;
using UnityEngine.Events;

public class AdInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    public UnityEvent EventAfterInitAds;
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] bool _testMode = true;
    private string _gameId;
    Action cbInit;

    //[SerializeField] Rewarded rewardedAdsButton;
    //[SerializeField] Interstitial intersitialAdsButton;

    public static AdInitializer adinits;


    void Awake()
    {
        if(adinits == null)
        {
            adinits = this;
        }
        DontDestroyOnLoad(this);
        if (adinits != this) 
        {
            Destroy(this.gameObject);
        }

       

    }

    private void Start()
    {
        InitializeAds();
    }

    public void InitializeAds()
    {
#if UNITY_IOS
            _gameId = _iOSGameId;
#elif UNITY_ANDROID
        _gameId = _androidGameId;
#elif UNITY_EDITOR
            _gameId = _androidGameId; //Only for testing the functionality in the Editor
#endif
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_gameId, _testMode, this);
        }
    }


    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        EventAfterInitAds?.Invoke();
        ///intersitialAdsButton.LoadAd();
        //Interstitial.inter.LoadAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}