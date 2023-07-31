using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using Random = UnityEngine.Random;


public class UnityAdsController : MonoBehaviour
{


    public string gameId;
    private string banner_placementId = "bannerAds";
    private string rewarded_placementId = "rewardedVideo";
    private string interstitial_placementId = "interstitial";
    private string video_placementId = "video";
    public bool testMode = true;
    public static UnityAdsController instance;
   public bool rewardedVideoFinished;
    

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }


    void Start()
    {

        Advertisement.Initialize(gameId, testMode);


    }

    public void ShowBanner()
    {
        StartCoroutine(ShowBannerWhenReady());
    }
    public void ShowInterstitial()
    {
        Debug.Log("ShowInterstitial");
        Advertisement.Show(interstitial_placementId);
        StartCoroutine(ShowInterstitialWhenReady());
    }
    public void ShowVideoAds()
    {
        StartCoroutine(ShowVideoAdWhenReady());
    }
    public void ShowRewardedVideo()
    {
        rewardedVideoFinished = false;
            StartCoroutine(ShowRewardedVideoWhenReady());
    }
    public int GameOverPerAds;
  public int a;
    public void RandomAds()
    {
        if (a < GameOverPerAds*2)
        {
       
            if (a == GameOverPerAds)
            {

                ShowInterstitial();
            }

        }
        else
        {
            ShowVideoAds();
            a = 0;
        }
    
    }



    IEnumerator ShowInterstitialWhenReady()
    {
        /*  while (!Advertisement.IsReady(interstitial_placementId))
          {
              Debug.Log("ShowInterstitial wait");

              yield return new WaitForSeconds(0.5f);
          }
          */
        yield return new WaitForSeconds(0.5f);
        Debug.Log("ShowingInterstitial()");

       // Advertisement.Show(interstitial_placementId);
    }

    IEnumerator ShowBannerWhenReady()
    {
        while (!Advertisement.IsReady(banner_placementId))
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Show(banner_placementId);
    }

    IEnumerator ShowVideoAdWhenReady()
    {
        while (!Advertisement.IsReady(video_placementId))
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Show(video_placementId);
    }

    IEnumerator ShowRewardedVideoWhenReady()
    {
        while (!Advertisement.IsReady(rewarded_placementId))
        {
            yield return new WaitForSeconds(0.5f);
        }
        ShowAd();
    }

    void ShowAd()
    {
        // menampilkan iklan        
        ShowOptions options = new ShowOptions();
        options.resultCallback = HandleShowResult;
        Advertisement.Show(rewarded_placementId, options);
    }

    void HandleShowResult(ShowResult result)
    {
        // merespon feedback, jika berhasil maka coin akan ditambah 50        
        if (result == ShowResult.Finished)
        {
            rewardedVideoFinished = true;
            Debug.Log("Video selesai - tawarkan coin ke pemain");

            //COIN BERTAMBAH ATAU LIFE TAMBAH ATAU YANG LAIN TARUH DISINI
        }
        else if (result == ShowResult.Skipped)
        {
            Debug.LogWarning("Video dilewati - tidak menawarkan coin ke pemain");
            rewardedVideoFinished = false;
        }
        else if (result == ShowResult.Failed)
        {
            Debug.LogError("Video tidak ditampilkan");
            rewardedVideoFinished = false;
        }
    }
    public bool getHasil() {


        return rewardedVideoFinished;
    }
    

}