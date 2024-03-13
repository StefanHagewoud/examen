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
        S_Weapon currentWeapon = pickupManager.gunHolderSecondary.transform.GetComponentInChildren<S_Weapon>();
        currentAmmoText.text = currentWeapon.magAmmo.ToString();
        currentMaxAmmoText.text = currentWeapon.maxMagAmmo.ToString();
    }
}
