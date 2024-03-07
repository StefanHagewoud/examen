using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class S_ScoreManager : MonoBehaviour
{
    public int score;
    public TMP_Text scoreText;
    public TMP_Text highscoreText;
    public static S_ScoreManager instance;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
        scoreText.text = score.ToString();
    }

    public void AddScore(int newScore)
    {
        score += newScore;
        scoreText.text = newScore.ToString();
        if (score > PlayerPrefs.GetInt("Highscore", score))
        {
            PlayerPrefs.SetInt("Highscore", score);
            SaveScore();
        }
    }
    public void SaveScore()
    {
        PlayerPrefs.GetInt("Highscore", score);
        highscoreText.text = PlayerPrefs.GetInt("Highscore", score).ToString();
    }
}
