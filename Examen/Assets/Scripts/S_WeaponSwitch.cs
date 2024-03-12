using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_WeaponSwitch : MonoBehaviour//Add every variable to diagram
{
    [Header("Weapon Primary")]
    public bool hasPrimary;
    public GameObject primaryWeaponHolder;
    public GameObject primaryWeapon;

    [Header("Weapon Secondary")]//Secondary cannot be dropped
    public GameObject secondaryWeapon;

    [Header("Weapons")]
    public GameObject primaryWeaponUIUnselected;
    public List<WeaponPrefab> weaponPrefabs = new List<WeaponPrefab>(new WeaponPrefab[1]);

    [Header("WeaponDrop")]
    public Transform weaponDropPosition;

    [Header("Scripts")]
    public S_UiManager uiManagerScript;

    [Header("Debug")]
    public bool allowDebug;

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
    }
    public void PickupGun(GameObject weaponGameObject)//Add diagram
    {
        if (hasPrimary)
        {
            if (allowDebug)
            {
                print("Player already has primary weapon: " + primaryWeapon.name);
            }
            return;
        }
        hasPrimary = true;

        GameObject parentOfWeapon = weaponGameObject.transform.parent.gameObject;
        primaryWeapon = weaponGameObject;
        weaponGameObject.GetComponent<MeshRenderer>().enabled = false;
        weaponGameObject.transform.position = primaryWeaponHolder.transform.position;
        weaponGameObject.transform.rotation = primaryWeaponHolder.transform.rotation;
        weaponGameObject.transform.parent = primaryWeaponHolder.transform;

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
        if (!hasPrimary)
        {
            print("Player has no primary weapon!");
            return;
        }
        for (int i = 0; i < weaponPrefabs.Count; i++)
        {
            if (weaponPrefabs[i].prefabWeapon.name == primaryWeapon.name)
            {
                weaponPrefabs[i].weaponUISelected.SetActive(true);
                weaponPrefabs[i].visualWeapon.SetActive(true);
            }
            else
            {
                weaponPrefabs[i].weaponUIUnselected.SetActive(false);
                weaponPrefabs[i].visualWeapon.SetActive(false);
            }
        }
        if (allowDebug)
        {
            print("Selected primary gun");
        }
    }
    public void SelectSecondaryGun()//Add diagram
    {
        for (int i = 0; i < weaponPrefabs.Count; i++)
        {
            if (weaponPrefabs[i].prefabWeapon.name == secondaryWeapon.name)
            {
                weaponPrefabs[i].weaponUISelected.SetActive(true);
                weaponPrefabs[i].visualWeapon.SetActive(true);
            }
            else
            {
                weaponPrefabs[i].weaponUIUnselected.SetActive(false);
                weaponPrefabs[i].visualWeapon.SetActive(false);
            }
        }
        if (allowDebug)
        {
            print("Selected secondary gun");
        }
    }
    public void DropWeapon()
    {
        if (!hasPrimary)
        {
            if (allowDebug)
            {
                print("Player has no primary weapon!");
            }
            return;
        }
        for (int i = 0; i < weaponPrefabs.Count; i++)
        {
            if (weaponPrefabs[i].prefabWeapon.name == primaryWeapon.name)
            {
                weaponPrefabs[i].weaponUI.SetActive(false);
                Instantiate(weaponPrefabs[i].prefabPickupableWeapon, weaponDropPosition.position, Quaternion.identity);
                Destroy(primaryWeaponHolder.transform.GetChild(0).gameObject);
                primaryWeapon = null;
                hasPrimary = false;
                return;
            }
        }
        
    }
    public bool CheckWeaponSpace()
    {
        bool hasSpace = true;

        if(hasPrimary)
        {
            hasSpace = false;
        }
        return hasSpace; 
    }
}
