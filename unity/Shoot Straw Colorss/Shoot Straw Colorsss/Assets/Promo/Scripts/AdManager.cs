using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Advertisements;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour 
{
	
	[Header("Next Scene To Load")]
	public string NextSceneToload;
	public float timetoload;
	[Header("XML Url")]
	public string AllCommonUrl;
	private bool IsMenuAdOpened;
	public bool IsPortrait;
	public bool IsMenuLoaded = false;
	
	
	public static AdManager instance;
	public bool IconLoaded;
	
	void Awake ()
	{
		if (instance) 
		{
			DestroyImmediate (gameObject);
		} 
		else 
		{
			DontDestroyOnLoad (gameObject);
			instance = this;
		}
	//	Advertisement.Initialize (UnityGameID, true);


	}
	void OnEnable(){


	}

	// Use this for initialization
	void Start ()
	{
		
		
		gameObject.name = "AdManager";
		loadRewardVideo ();
		loadInterstitial ();
		showBannerAd ();
		rewardBasedVideo.OnAdRewarded += onRewardedVideoEvent;
		rewardBasedVideo.OnAdClosed += onadclosed;


	}



	
	[Header("Unity ID")]
	[SerializeField] string UnityGameID = "33675";


	public void ShowAd(string zone = "")
	{
		#if UNITY_EDITOR
		StartCoroutine(WaitForAd ());
		#endif

		if (string.Equals (zone, ""))
			zone = null;

		ShowOptions options = new ShowOptions ();
		options.resultCallback = AdCallbackhandler;

		if (Advertisement.IsReady (zone)) {
			Advertisement.Show (zone, options);
		} else {
			showRewardVideo ();
		}
	}

	void AdCallbackhandler (ShowResult result)
	{
		switch(result)
		{
		case ShowResult.Finished:
			Debug.Log ("Ad Finished. Rewarding player...");
			break;
		case ShowResult.Skipped:
			Debug.Log ("Ad skipped. Son, I am dissapointed in you");
			break;
		case ShowResult.Failed:
			Debug.Log("I swear this has never happened to me before");
			break;
		}
	}

	IEnumerator WaitForAd()
	{
		float currentTimeScale = Time.timeScale;
		Time.timeScale = 0f;
		yield return null;

		while (Advertisement.isShowing)
			yield return null;

		Time.timeScale = currentTimeScale;
	}
	[Header("Admob Id")]
	public string bannerAdId, interstitialAdId, rewardVideoAdId;
	public bool  isDebug;
	[Header("BannerPos")]
	public  bool isOnTop;
	public static RewardBasedVideoAd rewardBasedVideo;
	private static BannerView bannerView;
	private static InterstitialAd interstitial ;
	public int adscount;

	void OnGUI ()
	{
		if (isDebug) {
			if (GUI.Button (new Rect (20, 0, 100, 60), "Load Full")) {
				loadInterstitial ();
			}
			if (GUI.Button (new Rect (20, 80, 100, 60), "Load Video")) {
				loadRewardVideo ();
			}
			if (GUI.Button (new Rect (20, 160, 100, 60), "Show Banner")) {
				showBannerAd ();
			}
			if (GUI.Button (new Rect (200, 0, 100, 60), "Show Full")) {
				showInterstitial ();
			}
			if (GUI.Button (new Rect (200, 80, 100, 60), "Show Video")) {
				showRewardVideo ();
			}
			if (GUI.Button (new Rect (200, 160, 100, 60), "Hide Banner")) {
				hideBannerAd ();
			}


		}
	}

	void onInterstitialEvent (object sender, System.EventArgs args)
	{
		print("OnAdLoaded event received.");
		// Handle the ad loaded event.
	}
	void onInterstitialCloseEvent (object sender, System.EventArgs args)
	{
		print("OnAdLoaded event received.");
		// Handle the ad loaded event.
	}

	void onBannerEvent (string eventName, string msg)
	{

	}
	void onadclosed(object sender,System.EventArgs args){
		//		ForLives = false;
	}
	void onRewardedVideoEvent (object sender, Reward args)
	{
		string type = args.Type;
		double amount = args.Amount;
		print("User rewarded with: " + amount.ToString() + " " + type);

	}
	public  void showBannerAd ()
	{

		if(isOnTop)
		{
			bannerView = new BannerView(bannerAdId, AdSize.Banner, AdPosition.Top);
			AdRequest request = new AdRequest.Builder().Build();
			// Load the banner with the request.
			bannerView.LoadAd(request);
		}
		else
		{
			bannerView = new BannerView(bannerAdId, AdSize.Banner, AdPosition.Bottom);
			AdRequest request = new AdRequest.Builder().Build();
			// Load the banner with the request.
			bannerView.LoadAd(request);
		}

		// Create an empty ad request.
	}
	public  void loadInterstitial ()
	{

		interstitial = new InterstitialAd(interstitialAdId);
		AdRequest request = new AdRequest.Builder().Build();
		// Load the interstitial with the request.
		interstitial.LoadAd(request);
		interstitial.OnAdOpening += onInterstitialEvent;
		interstitial.OnAdClosed += onInterstitialCloseEvent;

	}
	public  void showInterstitial ()
	{		

		if (interstitial.IsLoaded()) {
			interstitial.Show();

		}
		else
		{
			loadInterstitial ();
		}

	}
	public  void loadRewardVideo ()
	{

		rewardBasedVideo = RewardBasedVideoAd.Instance;

		AdRequest request = new AdRequest.Builder().Build();
		rewardBasedVideo.LoadAd(request, rewardVideoAdId);

	}
	public  void showRewardVideo ()
	{

		if (rewardBasedVideo.IsLoaded())
		{
			rewardBasedVideo.Show();			
		}
		else
		{
			loadRewardVideo ();
		}
	}
	public  void hideBannerAd ()
	{
		bannerView.Hide();
	}
}



