using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_ChangeScene : MonoBehaviour//Gemaakt door Ruben, zodat je naar verschillende scenes toe kan gaan.
{
    public void ChangeSceneInt(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
    public void ChangeSceneString(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
