using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalScore : MonoBehaviour
{
    Text TotalScoreZ;
    public static int totalValue;

    // Start is called before the first frame update
    void Start()
    {
        TotalScoreZ = GetComponent<Text>();
        totalValue += Score.ScoreValue;
        
    }

    // Update is called once per frame
    void Update()
    {
        TotalScoreZ.text = "TotalScore : " + totalValue;
        
    }

    //void kalah()
    //{
      //  if(totalValue > PlayerPrefs.GetFloat("TotalScore"))
        //{
        //    PlayerPrefs.SetFloat("TotalScore", totalValue);
       // }
    //}

   
}
