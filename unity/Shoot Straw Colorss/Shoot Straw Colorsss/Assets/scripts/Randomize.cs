using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Advertisements;

public class Randomize : MonoBehaviour
{
    Interstitial inters;
    private int randomNumber;
    public static Randomize randomizee;

    public void Randomz()
    {
        int[] numbers = { 1, 2 };
        int randomIndex = Random.Range(0, numbers.Length);
        int randomNumber = numbers[randomIndex];

        Debug.Log(randomNumber);

        if(randomNumber == 1)
        {
            
            Debug.Log("num 1 !!!!!!!!!!!!!!!!!!!!!");
            Adrand();
            //ShowAdss();
        }

    }

    public void Adrand()
    {
        //AdInitializer.adinits.OnInitializationComplete();
        Interstitial.inter.ShowAd();
        //AdInitializer.adinits.InitializeAds();
        //inters.ShowAd();
        //Advertisement.Show("intertitial");
        Debug.Log("adrand");
    }

    //public void ShowAdss()
    //{
    //    //inters.ShowAd();
    //    Debug.Log("swad");
    //}

}
