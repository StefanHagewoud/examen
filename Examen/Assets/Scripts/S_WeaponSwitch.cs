using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_WeaponSwitch : MonoBehaviour
{
    [Header("Weapon Primary")]
    public bool hasPrimary;
    public GameObject primaryWeaponHolder;
    public GameObject primaryWeapon;

    [Header("Weapon Secondary")]
    public bool hasSecondary;
    public GameObject secondaryWeaponHolder;
    public GameObject secondaryWeapon;

    [Header("Weapons")]
    public GameObject primaryWeaponUIUnselected;
    public List<WeaponPrefab> weaponPrefabs = new List<WeaponPrefab>(new WeaponPrefab[1]);

    [Serializable]
    public class WeaponPrefab//Add diagram
    {
        public string weaponName;
        public GameObject prefabWeapon;//Using this to compare if it's the right weapon
        public GameObject weaponUIUnselected;
        public GameObject weaponUISelected;
    }
    public void PickupGun(GameObject weaponGameObject)//Add diagram
    {
        
    }
    public void SelectLeftGun()//Add diagram
    {

    }
    public void SelectRightGun()//Add diagram
    {

    }
    public void DropWeapon()
    {

    }
    public bool CheckWeaponSpace()
    {
        bool hasSpace = true;

        if(hasPrimary && hasSecondary)
        {
            hasSpace = false;
        }
        return hasSpace; 
    }
}
