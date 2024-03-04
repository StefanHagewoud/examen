using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public Vector2 movementInput;
    public Transform playerTransform;
    public Transform cameraTransformRotation;
    public float walkMovementMultiplier = 1;
    public bool allowAnyMovement;

    [Header("Roll")]
    public float rollMovementMultiplier = 1;
    private bool isRolling;
    private Vector2 rollDirection;

    private bool allowUsingRoll = true;
    public float rollTime = 0.6f;
    public float rechargeRollTime = 3;

    [Header("Aim")]
    public float aimMovementMultiplier = 1;
    public Vector2 aimDirection;

    [Header("Debug")]
    public bool allowDebug;

    private void FixedUpdate()
    {
        if (!allowAnyMovement)
        {
            return;
        }

        if (!isRolling)// While player is not rolling he can rotate
        {
            playerTransform.Translate(new Vector3(movementInput.x, 0, movementInput.y) * walkMovementMultiplier * Time.fixedDeltaTime, cameraTransformRotation);
            
            if (Mathf.Abs(aimDirection.x) > 0.1f || Mathf.Abs(aimDirection.y) > 0.1f)
            {
                Vector3 playerDirection = Vector3.right * aimDirection.x + Vector3.forward * aimDirection.y;

                if (playerDirection.sqrMagnitude > 0)
                {
                    Quaternion calculateRotation = Quaternion.LookRotation(playerDirection, Vector3.up);
                    playerTransform.rotation = Quaternion.RotateTowards(playerTransform.rotation, calculateRotation, aimMovementMultiplier * Time.fixedDeltaTime);
                }
            }
        }
        else// player is rolling
        {
            playerTransform.Translate(new Vector3(rollDirection.x, 0, rollDirection.y) * rollMovementMultiplier * Time.fixedDeltaTime, cameraTransformRotation);
        } 
    }
    

    public IEnumerator ActivateRollCountdown()
    {
        if (isRolling || !allowUsingRoll || !allowAnyMovement)
        {
            print("Already activated roll!");
        }
        else
        {
            allowUsingRoll = false;
            rollDirection = movementInput;
            isRolling = true;

            yield return new WaitForSeconds(rollTime);
            isRolling = false;
            rollDirection = Vector3.zero;

            yield return new WaitForSeconds(rechargeRollTime - rollTime);
            allowUsingRoll = true;
        }
        yield return null;
    }
}
