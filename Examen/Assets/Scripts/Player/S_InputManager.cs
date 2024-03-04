using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class S_InputManager : MonoBehaviour
{
    private Vector2 moveVector;
    private Vector2 aimVector;
    private int meleeInput;
    private int rollInput;
    private int interactInput;
    private int rightWeaponInput;
    private int leftWeaponInput;
    private int settingsMenuInput;
    private int weaponDropInput;//Staat niet in de code diagram
    private int shootInput;

    [Header("Debug")]
    public bool allowDebug;

    [Header("Scripts")]
    public S_PlayerMovement playerMovementScript;

    public void OnMove(InputAction.CallbackContext value)
    {
        playerMovementScript.movementInput = value.ReadValue<Vector2>();

        if (value.performed)
        {
            if (allowDebug)
            {
                print(value.ReadValue<Vector2>() + "Performed, moveVector");
            }
        }
        if (value.canceled)
        {
            if (allowDebug)
            {
                print(value.ReadValue<Vector2>() + "Canceled, moveVector");
            }
        }
    }
    public void OnRoll(InputAction.CallbackContext value)
    {
        rollInput = (int)value.ReadValue<float>();
        if (value.performed)
        {
            StartCoroutine(playerMovementScript.ActivateRollCountdown());
            if (allowDebug)
            {
                print(value.ReadValue<float>() + "Performed, rollInput");
            }
        }
        if (value.canceled)
        {
            if (allowDebug)
            {
                print(value.ReadValue<float>() + "Canceled, rollInput");
            }
        }
    }
    public void OnAim(InputAction.CallbackContext value)
    {
        aimVector = value.ReadValue<Vector2>();
        playerMovementScript.aimDirection = aimVector;
        if (value.performed)
        {
            if (allowDebug)
            {
                print(value.ReadValue<Vector2>() + "Performed, aimVector");
            }
        }
        if (value.canceled)
        {
            if (allowDebug)
            {
                print(value.ReadValue<Vector2>() + "Canceled, aimVector");
            }
        }
    }
    public void OnInteract(InputAction.CallbackContext value)
    {
        interactInput = (int)value.ReadValue<float>();
        if (value.performed)
        {
            if (allowDebug)
            {
                print(value.ReadValue<float>() + "Performed, interactInput");
            }
        }
        if (value.canceled)
        {
            if (allowDebug)
            {
                print(value.ReadValue<float>() + "Canceled, interactInput");
            }
        }
    }
    public void OnMelee(InputAction.CallbackContext value)
    {
        meleeInput = (int)value.ReadValue<float>();
        if (value.performed)
        {
            if (allowDebug)
            {
                print(value.ReadValue<float>() + "Performed, meleeInput");
            }
        }
        if (value.canceled)
        {
            if (allowDebug)
            {
                print(value.ReadValue<float>() + "Canceled, meleeInput");
            }
        }
    }
    public void OnWeaponDrop(InputAction.CallbackContext value)
    {
        weaponDropInput = (int)value.ReadValue<float>();
        if (value.performed)
        {
            if (allowDebug)
            {
                print(value.ReadValue<float>() + "Performed, weaponDropInput");
            }
        }
        if (value.canceled)
        {
            if (allowDebug)
            {
                print(value.ReadValue<float>() + "Canceled, weaponDropInput");
            }
        }
    }
    public void OnRightWeapon(InputAction.CallbackContext value)
    {
        rightWeaponInput = (int)value.ReadValue<float>();
        if (value.performed)
        {
            if (allowDebug)
            {
                print(value.ReadValue<float>() + "Performed, rightWeaponInput");
            }
        }
        if (value.canceled)
        {
            if (allowDebug)
            {
                print(value.ReadValue<float>() + "Canceled, rightWeaponInput");
            }
        }
    }
    public void OnLeftWeapon(InputAction.CallbackContext value)
    {
        leftWeaponInput = (int)value.ReadValue<float>();
        if (value.performed)
        {
            if (allowDebug)
            {
                print(value.ReadValue<float>() + "Performed, leftWeaponInput");
            }
        }
        if (value.canceled)
        {
            if (allowDebug)
            {
                print(value.ReadValue<float>() + "Canceled, leftWeaponInput");
            }
        }
    }
    public void OnSettingsMenu(InputAction.CallbackContext value)
    {
        settingsMenuInput = (int)value.ReadValue<float>();
        if (value.performed)
        {
            if (allowDebug)
            {
                print(value.ReadValue<float>() + "Performed, settingsMenuInput");
            }
        }
        if (value.canceled)
        {
            if (allowDebug)
            {
                print(value.ReadValue<float>() + "Canceled, settingsMenuInput");
            }
        }
    }
    public void OnFire(InputAction.CallbackContext value)
    {
        shootInput = (int)value.ReadValue<float>();
        if (value.performed)// hoger dan 0.5 indrukken
        {
            if (allowDebug)
            {
                print(value.ReadValue<float>() + "Performed, shootInput");
            }
        }
        if (value.canceled)
        {
            if (allowDebug)
            {
                print(value.ReadValue<float>() + "Canceled, shootInput");
            }
        }
    }

}
