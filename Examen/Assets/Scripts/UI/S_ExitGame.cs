using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ExitGame : MonoBehaviour//Gemaakt door Ruben, zodat je de game kan uitgaan.
{
    public Animator exitAnimation;
    public float waitingTime;
    public void ExitGame()
    {
        exitAnimation.SetTrigger("GameStart");
        StartCoroutine(WaitingExitGame());
    }
    public IEnumerator WaitingExitGame()
    {
        yield return new WaitForSeconds(waitingTime);
        Application.Quit();
        yield return null;
    }
}
