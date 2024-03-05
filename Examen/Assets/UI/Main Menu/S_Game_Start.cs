using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_Game_Start : MonoBehaviour
{
    public Animator mainMenu;
    public float animationTime = 1.5f;
    public string sceneName;
    public void GameStart()
    {
        mainMenu.SetTrigger("GameStart");
        StartCoroutine(GameSceneDelay());
    }
    public IEnumerator GameSceneDelay()
    {
        yield return new WaitForSeconds(animationTime);
        SceneManager.LoadScene(sceneName);

        yield return null;
    }
}
