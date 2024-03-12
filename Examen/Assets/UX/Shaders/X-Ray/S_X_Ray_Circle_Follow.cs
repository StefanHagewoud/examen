using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_X_Ray_Circle_Follow : MonoBehaviour
{
    private static int positionCircle = Shader.PropertyToID("_Position");

    public Material wallMaterial;
    public Camera playerCamera;

    private void Start() {
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    void Update()
    {
        if(playerCamera == null) {
            playerCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }
        Vector3 view = playerCamera.WorldToViewportPoint(transform.position);
        wallMaterial.SetVector(positionCircle, view);
        
    }
}
