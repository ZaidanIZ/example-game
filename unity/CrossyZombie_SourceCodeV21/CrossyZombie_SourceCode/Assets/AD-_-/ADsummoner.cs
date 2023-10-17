using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(InterstitialAD))]
[RequireComponent(typeof(RewardedAD))]
[RequireComponent(typeof(BannerAD))]
[RequireComponent(typeof(ADinitializer))]

public class ADsummoner : MonoBehaviour
{
    public bool DestroyYn = true;
    public RewardedAD rewardz;
    public InterstitialAD interstitialz;
    public BannerAD bannerz;

    public static ADsummoner adSummoner;

    private void Awake()
    {
       

        if (adSummoner == null)
        {
            adSummoner = this;
        }
        if(DestroyYn == true)
        {
            DontDestroyOnLoad(this);
            if (adSummoner != this)
            {
                Destroy(this.gameObject);
            }
        }
        
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            interstitialz.LoadAd();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            interstitialz.ShowAd();
        }
    }

    //load
    public void LoadReward()
    {
        rewardz.LoadAd();
    }
    public void LoadReward(Action cb)
    {
        rewardz.LoadAd(cb);
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

    public void ShowReward(Action cb)
    {
        rewardz.ShowAd(cb);
    }

    public void ShowInterstitial()
    {
        interstitialz.ShowAd();
        Debug.Log("inter show");
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
