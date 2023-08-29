//using UnityEngine;
//using System.Collections.Generic;
//using System.Collections;
//using UnityEngine.SocialPlatforms;
//#if GOOGLE_MOBILE_ADS
//using GoogleMobileAds.Api;
//#endif
//using System;
//using UnityEngine.SocialPlatforms;

//public class AdsControl : MonoBehaviour
//{
//	public string yourAdmobKey;
//	protected AdsControl ()
//	{
//	}
//	#if GOOGLE_MOBILE_ADS
//	private BannerView bannerView;
//	InterstitialAd interstitial;
//	#endif

//	private static AdsControl _instance;

//	public static AdsControl Instance { get {
//			return _instance;
//		} }
	
//	void Awake ()
//	{
//		if (FindObjectsOfType (typeof(AdsControl)).Length > 1) {
//			Destroy (gameObject);
//			return;
//		}
		
//		_instance = this;
//		MakeNewAdmobAd ();
		
//		DontDestroyOnLoad (gameObject); //Already done by CBManager
//	}
	
//	public void HandleAdClosed (object sender, EventArgs args)
//	{
//		#if GOOGLE_MOBILE_ADS
//		if (interstitial != null)
//			interstitial.Destroy ();
//		MakeNewAdmobAd ();
//		#endif
//	}
	
//	void MakeNewAdmobAd ()
//	{
//		#if GOOGLE_MOBILE_ADS
//		interstitial = new InterstitialAd (yourAdmobKey);
//		interstitial.AdClosed += HandleAdClosed;
//		AdRequest request = new AdRequest.Builder ().Build ();
//		interstitial.LoadAd (request);
//		#endif
//	}
//	/// <summary>
//	///show interstial
//	/// </summary>
//	public void showAds ()
//	{
//		#if GOOGLE_MOBILE_ADS
//		Debug.Log ("Show ad");
//		if (interstitial != null && interstitial.IsLoaded ()) 
//			interstitial.Show ();
//		#endif
//	}
//	/// <summary>
//	///request Interstial
//	/// </summary>
//	private void RequestBanner ()
//	{
//		#if GOOGLE_MOBILE_ADS
//		string adUnitId = "ca-app-pub-6479628605402996/6968043261";
//		// Create a 320x50 banner at the top of the screen.
//		bannerView = new BannerView (adUnitId, AdSize.Banner, AdPosition.Bottom);
//		// Load a banner ad.
//		AdRequest request = new AdRequest.Builder ().Build ();
//		bannerView.LoadAd (request);
//		#endif
//	}

//	public void ShowBanner ()
//	{
//		#if GOOGLE_MOBILE_ADS
//		if (bannerView != null) 
//			bannerView.Show ();
//		#endif
//	}
//	public void HideBanner(){
//		#if GOOGLE_MOBILE_ADS
//	   bannerView.Hide ();
//		#endif
//	}
//}

