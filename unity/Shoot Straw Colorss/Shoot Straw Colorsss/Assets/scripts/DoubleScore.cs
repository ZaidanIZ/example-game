using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class DoubleScore : MonoBehaviour
{
    public GameObject panel2;
    DoubleScore dbsc;
    public void PlayRewardedAd()
    {
        Advertisement.Show("rewardedVideo");
        //score double ad\/
        Score.ScoreValue *= 2;
        TotalScore.totalValue += 108;
        Debug.Log("p");
        //panel2.SetActive(true);
        
    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Rewarded Ad Completed");
            // Grant a reward.
            //Score.ScoreValue *= 2;
        }
    }

    //score double tanpa ad\/
    public void tambah()
    {
       // Score.ScoreValue *= 2;
    }

    public void ilangPanel()
    {
        panel2.SetActive(false);
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
