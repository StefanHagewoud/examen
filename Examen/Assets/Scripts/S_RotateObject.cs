using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Rendering;

public class S_RotateObject : MonoBehaviour//Gemaakt door Ruben, zodat de powerups als powerups lijken.
{
    public Transform transformToMove;
    private Vector3 startPos;
    public float rotationSpeed = 0.5f;

    public float upDownSpeed = 0.05f;
    public float upDownMaxLength = 0.2f;
    private bool goingUp = true;

    private void Start()
    {
        startPos = transformToMove.position;
    }
    private void Update()
    {
        transformToMove.Rotate(new Vector3(0, rotationSpeed, 0));

        if (goingUp)
        {
            transformToMove.Translate(Vector3.up * upDownSpeed * Time.deltaTime);
            if(transformToMove.position.y - startPos.y >= upDownMaxLength)
            {
                goingUp = false;
            }
        }
        else
        {
            transformToMove.Translate(Vector3.down * upDownSpeed * Time.deltaTime);
            if (transformToMove.position.y - startPos.y <= -upDownMaxLength)
            {
                goingUp = true;
            }
        }
        
    }
}
