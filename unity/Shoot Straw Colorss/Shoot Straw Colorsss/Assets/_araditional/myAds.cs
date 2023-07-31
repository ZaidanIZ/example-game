using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myAds : MonoBehaviour
{
    UnityAdsController UAC;
 //   GMAS gm;
   public int countingToAds;
    public int perAds = 2;
    // Start is called before the first frame update
    public static myAds misal;
    private void Awake()
    {
        if (misal == null)
        {
            misal = this;
        }
        else {
            if (misal != this) {
                Destroy(this.gameObject);

            }
        }
    }
    void Start()
    {
        countingToAds = PlayerPrefs.GetInt("countAds", 0);
        UAC = UnityAdsController.instance;
    }
    private void OnEnable()
    {

      countingToAds=  PlayerPrefs.GetInt("countAds", 0);
        UAC = UnityAdsController.instance;
     //   gm = GMAS.Instance;
    }
    // Update is called once per frame
    void Update()
    {
        if (wasWatchForCoin && UAC.rewardedVideoFinished) {

            addCoin();
            wasWatchForCoin = false;
            UAC.rewardedVideoFinished = false;
        }
    }



    public void showAds()
    {
        Debug.Log("SeharusnyaAds");
        UAC.ShowVideoAds();
        // gm.ShowUnityAds();
    }
 

    public void CountToAds() {
        print("harusnyamasuk");
        countingToAds++;
        if (countingToAds >= perAds)
        {
            UAC.ShowVideoAds();
            countingToAds = 0;
        }
       
        PlayerPrefs.SetInt("countAds", countingToAds);

    }


    public int CoinPerAds;
    public bool wasWatchForCoin; 
    public void AdsForCoin() {
        UAC.ShowRewardedVideo();
        wasWatchForCoin = true;

    }
    
    void addCoin() {
     //   SgLib.CoinManager.Instance.AddCoins(CoinPerAds);
        Debug.Log("coinDitambah dari ads");
    }

}
