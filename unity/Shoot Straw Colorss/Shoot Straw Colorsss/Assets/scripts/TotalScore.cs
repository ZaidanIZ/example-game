using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalScore : MonoBehaviour
{
    public Text TotalScoreZ;
    public static int totalValue;
    //public float totalValue;

    // Start is called before the first frame update
    void Start()
    {

        //totalValue += Score.ScoreValue;
        Debug.Log("oi" + this.gameObject.name);
        TotalScoreZ.text = "Total Score :" + PlayerPrefs.GetInt("TotalScore", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    //void kalah()
    //{
      //  if(totalValue > PlayerPrefs.GetFloat("TotalScore"))
        //{
        //    PlayerPrefs.SetFloat("TotalScore", totalValue);
       // }
    //}

   
}
