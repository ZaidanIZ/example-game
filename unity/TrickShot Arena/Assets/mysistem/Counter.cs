using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    public Text scoreText;
    public Text totalScoreText; // Ganti highScore menjadi totalScore
    public Text scoreAkhirText;
    public Button startButton;

    private int score = 0;
    private int totalScore = 0; // Ganti highScore menjadi totalScore
    private int scoreAkhir = 0;
    private bool isCounting = false;

    void Start()
    {
        startButton.onClick.AddListener(StartCounting);
        totalScore = PlayerPrefs.GetInt("TotalScore", 0); // Load high score from PlayerPrefs, ganti highScore menjadi totalScore
        UpdateScoreText();
        UpdateTotalScoreText(); // Ganti highScore menjadi totalScore
        UpdateScoreAkhirText();
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
        scoreAkhir += 1;
        UpdateScoreText();
        UpdateScoreAkhirText();
    }

    void SaveAndResetScore()
    {
        totalScore += score; // Ganti highScore menjadi totalScore
        if (totalScore > PlayerPrefs.GetInt("TotalScore", 0)) // Check if new high score, ganti highScore menjadi totalScore
        {
            PlayerPrefs.SetInt("TotalScore", totalScore); // Save new high score to PlayerPrefs, ganti highScore menjadi totalScore
            PlayerPrefs.Save(); // Don't forget to save changes!
        }
        score = 0;
        UpdateScoreText();
        UpdateTotalScoreText(); // Ganti highScore menjadi totalScore
    }

    void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }

    void UpdateTotalScoreText() // Ganti highScore menjadi totalScore
    {
        totalScoreText.text = totalScore.ToString(); // Ganti highScore menjadi totalScore
    }

    void UpdateScoreAkhirText()
    {
        scoreAkhirText.text = scoreAkhir.ToString();
    }
}
