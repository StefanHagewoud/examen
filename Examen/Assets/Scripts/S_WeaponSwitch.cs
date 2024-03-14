using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class S_WeaponSwitch : MonoBehaviour//Add every variable to diagram
{
    [Header("Weapon Primary")]
    public bool usingPrimaryWeapon;
    public GameObject primaryWeapon;

    [Header("Weapon Secondary")]//Secondary cannot be dropped
    public bool usingSecondaryWeapon;
    public GameObject secondaryWeapon;

    [Header("Weapons")]
    public GameObject primaryWeaponUIUnselected;
    public List<WeaponPrefab> weaponPrefabs = new List<WeaponPrefab>(new WeaponPrefab[1]);

    [Header("WeaponDrop")]
    public Transform weaponDropPosition;

    [Header("Scripts")]
    public S_UiManager uiManagerScript;
    public S_PickupManager pickupManagerScript;

    [Header("Debug")]
    public bool allowDebug;

    [Header("Animator")]
    [SerializeField] private Animator animator;

    private void Start()
    {
        if (!uiManagerScript)
        {
            Debug.LogWarning("The variable uiManagerScript is not attached!", this);
        }
    }

    [Serializable]
    public class WeaponPrefab//Add diagram
    {
        public string weaponName;
        public GameObject visualWeapon;
        public GameObject prefabWeapon;//Using this to compare if it's the right weapon
        public GameObject prefabPickupableWeapon;//Using this to spawn in the weapon again
        public GameObject weaponUI;
        public GameObject weaponUIUnselected;
        public GameObject weaponUISelected;
        public TextMeshProUGUI crAmmoText;
        public TextMeshProUGUI maxAmmoText;
        public int layerIndex;
    }
    public void PickupGun(GameObject weaponGameObject)//Add diagram
    {
        if (primaryWeapon)
        {
            if (allowDebug)
            {
                print("Player already has primary weapon: " + primaryWeapon.name);
            }
            return;
        }

        weaponGameObject.GetComponent<S_Weapon>().animator = animator;

        GameObject parentOfWeapon = weaponGameObject.transform.parent.gameObject;
        primaryWeapon = weaponGameObject;

        foreach (var mesh in weaponGameObject.GetComponentsInChildren<MeshRenderer>())
        {
            mesh.enabled = false;
        }

        weaponGameObject.transform.position = pickupManagerScript.gunHolderPrimary.transform.position;
        weaponGameObject.transform.rotation = pickupManagerScript.gunHolderPrimary.transform.rotation;
        weaponGameObject.transform.parent = pickupManagerScript.gunHolderPrimary.transform;

        Destroy(parentOfWeapon);
        weaponGameObject.GetComponent<S_Weapon>().uiManager = uiManagerScript;

        for (int i = 0; i < weaponPrefabs.Count; i++)
        {
            if (weaponPrefabs[i].prefabWeapon.name == weaponGameObject.name)
            {
                weaponPrefabs[i].weaponUI.SetActive(true);
                weaponPrefabs[i].visualWeapon.SetActive(true);
                return;
            }
            else
            {
                weaponPrefabs[i].visualWeapon.SetActive(false);
            }
        }
        SelectPrimaryGun();

        if (allowDebug)
        {
            print("Picked up gun with: " + weaponGameObject.name);
        }
    }
    public void SelectPrimaryGun()//Add diagram
    {
        if (!primaryWeapon)
        {
            print("Player has no primary weapon!");
            return;
        }
        usingPrimaryWeapon = true;
        primaryWeapon.SetActive(true);
        usingSecondaryWeapon = false;
        secondaryWeapon.SetActive(false);

        for (int i = 0; i < weaponPrefabs.Count; i++)
        {
            if (weaponPrefabs[i].prefabWeapon.name == primaryWeapon.name)
            {
                weaponPrefabs[i].weaponUISelected.SetActive(true);
                weaponPrefabs[i].weaponUIUnselected.SetActive(false);
                weaponPrefabs[i].visualWeapon.SetActive(true);
                uiManagerScript.currentAmmoText = weaponPrefabs[i].crAmmoText;
                uiManagerScript.currentMaxAmmoText = weaponPrefabs[i].maxAmmoText;

                animator.SetLayerWeight(weaponPrefabs[i].layerIndex, 1);
            }
            else
            {
                weaponPrefabs[i].weaponUISelected.SetActive(false);
                weaponPrefabs[i].weaponUIUnselected.SetActive(true);
                weaponPrefabs[i].visualWeapon.SetActive(false);

                animator.SetLayerWeight(weaponPrefabs[i].layerIndex, 0);
            }
        }
        uiManagerScript.UpdateWeaponUI();

        if (allowDebug)
        {
            print("Selected primary gun");
        }
    }
    public void SelectSecondaryGun()//Add diagram
    {
        usingPrimaryWeapon = false;
        if (primaryWeapon)
        {
            primaryWeapon.SetActive(false);
        }

        usingSecondaryWeapon = true;
        secondaryWeapon.SetActive(true);

        for (int i = 0; i < weaponPrefabs.Count; i++)
        {
            if (weaponPrefabs[i].prefabWeapon.name == secondaryWeapon.name)
            {
                weaponPrefabs[i].weaponUISelected.SetActive(true);
                weaponPrefabs[i].weaponUIUnselected.SetActive(false);
                weaponPrefabs[i].visualWeapon.SetActive(true);
                uiManagerScript.currentAmmoText = weaponPrefabs[i].crAmmoText;
                uiManagerScript.currentMaxAmmoText = weaponPrefabs[i].maxAmmoText;

                animator.SetLayerWeight(weaponPrefabs[i].layerIndex, 1);
            }
            else
            {
                weaponPrefabs[i].weaponUISelected.SetActive(false);
                weaponPrefabs[i].weaponUIUnselected.SetActive(true);
                weaponPrefabs[i].visualWeapon.SetActive(false);

                animator.SetLayerWeight(weaponPrefabs[i].layerIndex, 0);
            }
        }
        uiManagerScript.UpdateWeaponUI();

        if (allowDebug)
        {
            print("Selected secondary gun");
        }
    }
    public void DropWeapon()
    {
        if (!primaryWeapon)
        {
            if (allowDebug)
            {
                print("Player has no primary weapon!");
            }
            return;
        }
        for (int i = 0; i < weaponPrefabs.Count; i++)
        {
            if (weaponPrefabs[i].prefabWeapon.name == primaryWeapon.name)//Makes a pickupable weapon and destroys the weapon on the player
            {
                weaponPrefabs[i].weaponUI.SetActive(false);

                GameObject instantiatedGameObject = Instantiate(weaponPrefabs[i].prefabPickupableWeapon, weaponDropPosition.position, Quaternion.identity);
                instantiatedGameObject.GetComponentInChildren<S_Weapon>().magAmmo = primaryWeapon.GetComponent<S_Weapon>().magAmmo;

                Destroy(pickupManagerScript.gunHolderPrimary.transform.GetChild(0).gameObject);
                primaryWeapon = null;

                SelectSecondaryGun();
                return;
            }
        }
        
    }
    public bool CheckWeaponSpace()
    {
        bool hasSpace = true;

        if(primaryWeapon)
        {
            hasSpace = false;
            if (allowDebug)
            {
                print("There is no space for a primary weapon!");
            }
        }
        return hasSpace; 
    }
}
