using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_Game_Start : MonoBehaviour
{
    public Animator mainMenu;
    public Animator auto1Cutscene;
    public Animator auto2Cutscene;
    public Animator autoPolitieCutscene;
    public float animationTime;
    public GameObject player;


    public void GameStart()
    {
        mainMenu.SetTrigger("GameStart");
        auto1Cutscene.SetTrigger("Auto Stop");
        auto2Cutscene.SetTrigger("Auto Stop");
        autoPolitieCutscene.SetTrigger("IntroStart");
        StartCoroutine(GameSceneDelay());
    }
    public IEnumerator GameSceneDelay()
    {
        yield return new WaitForSeconds(animationTime);
        player.SetActive(true);


        yield return null;
    }
}
