using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class DoubleScore : MonoBehaviour
{
    DoubleScore dbsc;
    public void PlayRewardedAd()
    {
        Advertisement.Show("rewardedVideo");
        //score double ad\/
        Score.ScoreValue *= 2;
        //TotalScore.totalValue += Score.ScoreValue * 2;
        Debug.Log("p");
        
    }

    //score double tanpa ad\/
    public void tambah()
    {
        Score.ScoreValue *= 2;
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
