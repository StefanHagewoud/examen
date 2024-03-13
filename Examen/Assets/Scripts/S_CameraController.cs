using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_CameraController : MonoBehaviour//Gemaakt door Ruben
{
    public Camera playerCamera;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneSwitch;
    }

    void OnSceneSwitch(Scene scene, LoadSceneMode loadSceneMode)
    {
        Transform cameraToGoTransform = GameObject.Find("CameraPos").transform;
        playerCamera.transform.position = cameraToGoTransform.position;
        playerCamera.transform.rotation = cameraToGoTransform.rotation;
    }
}
