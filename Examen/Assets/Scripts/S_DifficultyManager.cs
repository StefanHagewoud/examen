using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_DifficultyManager : MonoBehaviour
{
    public enum Difficulties { Easy, Medium, Hard };//Add Diagram.
    public S_ScriptableObjectDifficulty scriptableObjectDifficulty;//Add Diagram.
    public List<Difficulty> difficultiesSettings = new List<Difficulty>(new Difficulty[3]);//Add Diagram.

    [Header("Buttons to change")]
    public Button easyDifficultyButton;
    public Button mediumDifficultyButton;
    public Button hardDifficultyButton;

    [Serializable]//Add Diagram.
    public class Difficulty//Add Diagram.
    {
        public Difficulties difficulty;
        public float enemyHealthMultiplier;
        public float playerHealthMultiplier;
    }
    private void Start()
    {
        if(easyDifficultyButton && mediumDifficultyButton && hardDifficultyButton)
        {
            GetDifficulty();
        }
        
    }

    public void SetDifficulty(int difficultyNumber)//Add Diagram.
    {
        switch (difficultyNumber)
        {
            case 0:
                scriptableObjectDifficulty.difficulty.difficulty = Difficulties.Easy;
                scriptableObjectDifficulty.difficulty.enemyHealthMultiplier = difficultiesSettings[0].enemyHealthMultiplier;
                scriptableObjectDifficulty.difficulty.playerHealthMultiplier = difficultiesSettings[0].playerHealthMultiplier;
                break;
            case 1:
                scriptableObjectDifficulty.difficulty.difficulty = Difficulties.Medium;
                scriptableObjectDifficulty.difficulty.enemyHealthMultiplier = difficultiesSettings[1].enemyHealthMultiplier;
                scriptableObjectDifficulty.difficulty.playerHealthMultiplier = difficultiesSettings[1].playerHealthMultiplier;
                break;
            case 2:
                scriptableObjectDifficulty.difficulty.difficulty = Difficulties.Hard;
                scriptableObjectDifficulty.difficulty.enemyHealthMultiplier = difficultiesSettings[2].enemyHealthMultiplier;
                scriptableObjectDifficulty.difficulty.playerHealthMultiplier = difficultiesSettings[2].playerHealthMultiplier;
                break;
            default:
                Debug.LogWarning("difficultyNumber is out of range!");
                break;
        }
    }
    public void GetDifficulty()//Add Diagram.
    {
        switch (scriptableObjectDifficulty.difficulty.difficulty)
        {
            case Difficulties.Easy:
                easyDifficultyButton.gameObject.SetActive(true);
                break;
            case Difficulties.Medium:
                mediumDifficultyButton.gameObject.SetActive(true);
                break;
            case Difficulties.Hard:
                hardDifficultyButton.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }
}

