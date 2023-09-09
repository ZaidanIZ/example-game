using UnityEngine;
using System.Collections;

public class Muncul3Min : MonoBehaviour
{
    //public GameObject myObject;

    void Start()
    {
        StartCoroutine(WaitAndPrint(180.0F));
        Debug.Log("CountDown 3 min");
    }

    IEnumerator WaitAndPrint(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            InterstitialAD.interstitialAD.ShowAd();
            Debug.Log("muncul ads");
        }
    }
}