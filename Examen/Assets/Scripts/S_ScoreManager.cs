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
    public static S_ScoreManager instance;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
        scoreText.text = score.ToString();
        PlayerPrefs.GetInt("Highscore", 0);
        SaveScore();
    }

    public void AddScore(int newScore)
    {
        score += newScore;
        scoreText.text = score.ToString();
        if (score > PlayerPrefs.GetInt("Highscore", 0))
        {
            PlayerPrefs.SetInt("Highscore", score);
            SaveScore();
        }
    }
    public void SaveScore()
    {
        highscoreText.text = PlayerPrefs.GetInt("Highscore", 0).ToString();
    }
}
