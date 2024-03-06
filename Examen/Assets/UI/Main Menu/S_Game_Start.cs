using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Game_Start : MonoBehaviour
{
    public Animator mainMenu;
    public Animator auto1Cutscene;
    public Animator auto2Cutscene;
    public Animator autoPolitieCutscene;


    public void GameStart()
    {
        mainMenu.SetTrigger("GameStart");
        auto1Cutscene.SetTrigger("Auto Stop");
        auto2Cutscene.SetTrigger("Auto Stop");
        autoPolitieCutscene.SetTrigger("IntroStart");
    }
}
