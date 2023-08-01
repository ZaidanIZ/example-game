using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoretest : MonoBehaviour
{
    public Text ScoreZz;
    public Text TotalScoreZz;
    public float ScoreValuez;
    public float TotalValuez;

    public void addScore()
    {
        ScoreValuez++;
    }

    // Start is called before the first frame update
    void Start()
    {
        TotalValuez = PlayerPrefs.GetFloat("highScore");
        //TotalValue = PlayerPrefs.GetFloat("TotalScore");
    }

    // Update is called once per frame
    void Update()
    {
        ScoreZz.text = ScoreValuez.ToString("Score : " + ScoreValuez);
        TotalScoreZz.text = TotalValuez.ToString("Highscore : " + TotalValuez);

        if (ScoreValuez > TotalValuez)
        {
            PlayerPrefs.SetFloat("highScore", ScoreValuez);
            //PlayerPrefs.GetFloat("highScore" + ScoreValue);
            //PlayerPrefs.SetFloat("highScore", +ScoreValue);
        }
    }
}
