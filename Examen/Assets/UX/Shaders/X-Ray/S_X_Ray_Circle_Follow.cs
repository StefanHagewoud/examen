using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_X_Ray_Circle_Follow : MonoBehaviour
{
    private static int positionCircle = Shader.PropertyToID("_Position");

    public Material wallMaterial;
    public Camera playerCamera;
    public LayerMask mask;
    public Transform player;


    void Update()
    {
        Vector3 view = playerCamera.WorldToViewportPoint(transform.position);
        wallMaterial.SetVector(positionCircle, view);
        
    }
}
