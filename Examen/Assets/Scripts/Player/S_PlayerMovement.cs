using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider))]
public class S_PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public Vector2 movementInput;
    public Transform playerTransform;
    public float walkMovementMultiplier = 1;
    public bool allowAnyMovement = true;
    private Transform cameraTransformRotation;
    [SerializeField]
    private Animator animator;

    [Header("Roll")]
    public float rollMovementMultiplier = 1;
    private bool isRolling;
    private Vector2 rollDirection;

    private bool allowUsingRoll = true;
    public float rollTime = 0.6f;
    public float rechargeRollTime = 3;

    private bool inRollAnimation = false;

    private BoxCollider rollTrigger;

    [Header("Aim")]
    public float aimMovementMultiplier = 1;
    public Vector2 aimDirection;

    [Header("Debug")]
    public bool allowDebug;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        rollTrigger = GetComponent<BoxCollider>();
        rollTrigger.isTrigger = true;
        rollTrigger.enabled = false;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)//Also triggers while starting
    {
        cameraTransformRotation = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }
    private void FixedUpdate()
    {
        if (!allowAnyMovement || !cameraTransformRotation)
        {
            return;
        }

        if (!isRolling)// While player is not rolling he can rotate
        {
            Vector3 newMovementDirection = Vector3.zero;
            if(Mathf.Abs(movementInput.x) + Mathf.Abs(movementInput.y) > 1)//Calculates so ther input is always 1 or lower.
            {
                newMovementDirection.x = Mathf.Abs(movementInput.x) / (Mathf.Abs(movementInput.x) + Mathf.Abs(movementInput.y));
                newMovementDirection.z = Mathf.Abs(movementInput.y) / (Mathf.Abs(movementInput.x) + Mathf.Abs(movementInput.y));
                if(movementInput.x < 0)//
                {
                    newMovementDirection.x = -newMovementDirection.x;
                }
                if(movementInput.y < 0)
                {
                    newMovementDirection.z = -newMovementDirection.z;
                }
            }
            else
            {
                newMovementDirection = new Vector3(movementInput.x, 0, movementInput.y);
            }
            playerTransform.Translate(new Vector3(newMovementDirection.x, 0, newMovementDirection.z) * walkMovementMultiplier * Time.fixedDeltaTime, cameraTransformRotation);

            if (Mathf.Abs(aimDirection.x) > 0.1f || Mathf.Abs(aimDirection.y) > 0.1f)
            {
                Vector3 playerDirection = Vector3.right * aimDirection.x + Vector3.forward * aimDirection.y;

                if (playerDirection.sqrMagnitude > 0)
                {
                    Quaternion calculateRotation = Quaternion.LookRotation(playerDirection, Vector3.up);
                    playerTransform.rotation = Quaternion.RotateTowards(playerTransform.rotation, calculateRotation, aimMovementMultiplier * Time.fixedDeltaTime);
                }
            }
            animator.SetFloat("X", newMovementDirection.x);
            animator.SetFloat("Y", newMovementDirection.z);
        }
        else// player is rolling
        {
            if (!inRollAnimation)
            {
               animator.SetTrigger("Roll");
               inRollAnimation = true;
            }
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
            rollTrigger.enabled = true;

            yield return new WaitForSeconds(rollTime);
            isRolling = false;
            rollTrigger.enabled = false;
            rollDirection = Vector3.zero;

            yield return new WaitForSeconds(rechargeRollTime - rollTime);
            allowUsingRoll = true;
            inRollAnimation = false;
        }
        yield return null;
    }
}
