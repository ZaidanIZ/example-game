using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static int ScoreValue = 0;
    Text ScoreZ;
    

   

    // Start is called before the first frame update
    void Start()
    {
        ScoreZ = GetComponent<Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //ScoreZ.text = "Score : " + ScoreValue;
        ScoreZ.text = "score : " + ScoreValue;
        //TotalScoreZ.text = "TotalScore : " + ScoreValue;
        //TotalScore.totalValue = ScoreValue;
        
        
    }
}
