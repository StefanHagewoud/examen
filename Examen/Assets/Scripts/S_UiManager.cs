using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class S_UiManager : MonoBehaviour
{
    public S_PickupManager pickupManager;
    public Transform hearts;
    public Transform armor;
    public TextMeshProUGUI currentAmmoText;
    public TextMeshProUGUI currentMaxAmmoText;

    public S_WeaponSwitch weaponSwitch;
    // Start is called before the first frame update
    void Start()
    {
        UpdateWeaponUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateWeaponUI() {
        Debug.Log("updating gun UI");
        if(weaponSwitch.usingPrimaryWeapon && weaponSwitch.primaryWeapon)
        {
            S_Weapon currentWeapon = pickupManager.gunHolderPrimary.transform.GetComponentInChildren<S_Weapon>();

            currentAmmoText.text = currentWeapon.magAmmo.ToString();
            currentMaxAmmoText.text = currentWeapon.maxMagAmmo.ToString();
        }
        if (weaponSwitch.usingSecondaryWeapon && weaponSwitch.secondaryWeapon)
        {
            S_Weapon currentWeapon = pickupManager.gunHolderSecondary.transform.GetComponentInChildren<S_Weapon>();

            currentAmmoText.text = currentWeapon.magAmmo.ToString();
            currentMaxAmmoText.text = currentWeapon.maxMagAmmo.ToString();
        }    
    }
}
