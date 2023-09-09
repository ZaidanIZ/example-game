using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADsummoner : MonoBehaviour
{
    public RewardedAD rewardz;
    public InterstitialAD interstitialz;
    public BannerAD bannerz;

    public static ADsummoner adSummoner;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //load
    public void LoadReward()
    {
        rewardz.LoadAd();
    }

    public void LoadInterstitial()
    {
        interstitialz.LoadAd();
    }

    public void LoadBanner()
    {
        bannerz.LoadBanner();
    }


    //show
    public void ShowReward()
    {
        rewardz.ShowAd();
    }

    public void ShowInterstitial()
    {
        interstitialz.ShowAd();
    }

    public void tesss()
    {
        Debug.Log("pe");
    }

    public void ShowBanner()
    {
        //bannerz.ShowBannerAd();
    }

    public void HideBanner()
    {
        //bannerz.HideBannerAd();
    }

}
