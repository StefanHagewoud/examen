using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Game_Start : MonoBehaviour
{
    public Animator mainMenu;

    public void GameStart()
    {
        mainMenu.SetTrigger("GameStart");
    }
}
