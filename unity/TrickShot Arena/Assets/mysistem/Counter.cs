using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    public Text scoreText;
    public Text highScoreText;
    public Text totalScoreText;
    public Button startButton;
    //public Button stopButton;

    private int score = 0;
    private int highScore = 0;
    private int totalScore = 0;
    private bool isCounting = false;

    void Start()
    {
        startButton.onClick.AddListener(StartCounting);
        //stopButton.onClick.AddListener(StopCounting);
        highScore = PlayerPrefs.GetInt("HighScore", 0); // Load high score from PlayerPrefs
        UpdateScoreText();
        UpdateHighScoreText();
        UpdateTotalScoreText();
    }

    void StartCounting()
    {
        if (!isCounting)
        {
            isCounting = true;
            InvokeRepeating("IncrementScore", 1.0f, 1.0f);
        }
    }

    public void StopCounting()
    {
        if (isCounting)
        {
            isCounting = false;
            CancelInvoke("IncrementScore");
            SaveAndResetScore();
        }
    }

    void IncrementScore()
    {
        score += 1;
        totalScore += 1;
        UpdateScoreText();
        UpdateTotalScoreText();
    }

    void SaveAndResetScore()
    {
        highScore += score;
        if (highScore > PlayerPrefs.GetInt("HighScore", 0)) // Check if new high score
        {
            PlayerPrefs.SetInt("HighScore", highScore); // Save new high score to PlayerPrefs
            PlayerPrefs.Save(); // Don't forget to save changes!
        }
        score = 0;
        UpdateScoreText();
        UpdateHighScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }

    void UpdateHighScoreText()
    {
        highScoreText.text = highScore.ToString();
    }

    void UpdateTotalScoreText()
    {
        totalScoreText.text = totalScore.ToString();
    }
}
