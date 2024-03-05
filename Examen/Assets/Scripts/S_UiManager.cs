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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateWeaponUI() {
        S_Weapon currentWeapon = pickupManager.gunHolderPrimary.transform.GetComponentInChildren<S_Weapon>();
        currentAmmoText.text = currentWeapon.magAmmo.ToString();
    }
}
