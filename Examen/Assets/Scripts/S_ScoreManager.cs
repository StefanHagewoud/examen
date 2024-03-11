using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class S_ScoreManager : MonoBehaviour
{
    public int score;
    [SerializeField]
    private int highscore;
    public TMP_Text scoreText;
    [SerializeField]
    private TMP_Text highscoreText;
    [SerializeField]
    private bool clearScore;
    private void Awake()
    {
        if (clearScore)
        {
            PlayerPrefs.DeleteKey("Score");
        }
        score = PlayerPrefs.GetInt("Score", score);
        scoreText.text = PlayerPrefs.GetInt("Score", score).ToString();
        PlayerPrefs.GetInt("Score", 0);
        highscore = PlayerPrefs.GetInt("Highscore", 0);
        highscoreText.text = PlayerPrefs.GetInt("Highscore", 0).ToString();
        SaveScore();
    }

    public void AddScore(int newScore)
    {
        score += newScore;
        scoreText.text = score.ToString();
        PlayerPrefs.SetInt("Score", score);
        if (score > PlayerPrefs.GetInt("Highscore", 0))
        {
            PlayerPrefs.SetInt("Highscore", score);
            SaveScore();
        }
    }
    public void SaveScore()
    {
        highscoreText.text = PlayerPrefs.GetInt("Highscore", 0).ToString();
        scoreText.text = PlayerPrefs.GetInt("Score", 0).ToString();
    }
}
