using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_DataSave : MonoBehaviour
{
    public S_Player player;
    public S_WeaponSwitch weaponSwitch;
    public S_PickupManager pickupManager;
    void Start() {
        LoadData(); // Load saved data when the game starts
    }

    void OnApplicationQuit() {
        PlayerPrefs.DeleteAll(); // Clear all player preferences
    }


    // Function to save data
    void SaveData() {
        PlayerPrefs.SetInt("Score", player.score);
        PlayerPrefs.SetFloat("PlayerHealth", player.health);
        PlayerPrefs.SetFloat("PlayerArmor", player.armor);
        if (weaponSwitch.primaryWeapon != null) {
            PlayerPrefs.SetString("PrimaryWeapon", weaponSwitch.primaryWeapon.name);
            PlayerPrefs.SetInt("PrimaryWeaponAmmo", weaponSwitch.primaryWeapon.GetComponent<S_Weapon>().magAmmo);
        }
        PlayerPrefs.SetInt("SecondaryWeaponAmmo", weaponSwitch.secondaryWeapon.GetComponent<S_Weapon>().magAmmo);
        PlayerPrefs.Save(); // Save to disk
    }

    // Function to load data
    void LoadData() {
        if (PlayerPrefs.HasKey("Score"))
            player.score = PlayerPrefs.GetInt("Score");

        if (PlayerPrefs.HasKey("PlayerHealth"))
            player.health = PlayerPrefs.GetFloat("PlayerHealth");

        if (PlayerPrefs.HasKey("PlayerArmor"))
            player.armor = PlayerPrefs.GetFloat("PlayerArmor");

        weaponSwitch.secondaryWeapon.GetComponent<S_Weapon>().magAmmo = PlayerPrefs.GetInt("SecondaryWeaponAmmo");

        for (int i = 0; i < weaponSwitch.weaponPrefabs.Count; i++) {
            Debug.Log(PlayerPrefs.GetString("PrimaryWeapon"));
            if (weaponSwitch.weaponPrefabs[i].prefabWeapon != null && PlayerPrefs.GetString("PrimaryWeapon") != "" && weaponSwitch.weaponPrefabs[i].prefabWeapon.name == PlayerPrefs.GetString("PrimaryWeapon")) {
                Debug.Log("trying to pickup" + weaponSwitch.weaponPrefabs[i].prefabWeapon.name);
                GameObject spawnedWeapon = Instantiate(weaponSwitch.weaponPrefabs[i].prefabPickupableWeapon);
                spawnedWeapon.transform.position = player.transform.position;
                //weaponSwitch.primaryWeapon.GetComponent<S_Weapon>().magAmmo = PlayerPrefs.GetInt("PrimaryWeaponAmmo");
            }
        }
    }
}
