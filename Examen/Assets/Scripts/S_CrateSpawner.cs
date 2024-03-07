using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class S_CrateSpawner : MonoBehaviour
{
    public UnityEvent OnCrateSpawned;

    public GameObject medkitPowerupPrefab;//Add Diagram
    public GameObject armorPowerupPrefab;//Add Diagram
    public GameObject rpgPowerupPrefab;//Add Diagram
    public List<GameObject> weaponsPrefab;//Add Diagram
    
    [Header("Debug")]
    public bool allowDebug;

    public void SpawnCrate()
    {
        int randomPowerupInt = Random.Range(0, 4);
        GameObject prefabToInstantiate = null;
        switch (randomPowerupInt)
        {
            case 0:
                prefabToInstantiate = medkitPowerupPrefab;
                break;
            case 1:
                prefabToInstantiate = armorPowerupPrefab;
                break;
            case 2:
                prefabToInstantiate = rpgPowerupPrefab;
                break;
            case 3://For every weapon
                int randomWeaponInt = Random.Range(0, weaponsPrefab.Count);
                prefabToInstantiate = weaponsPrefab[randomWeaponInt];
                break;
            default:
                UnityEngine.Debug.LogWarning("Nummer is out of range!", this);
                break;
        }

        Instantiate(prefabToInstantiate, transform.position, Quaternion.identity, transform);

        OnCrateSpawned.Invoke();
        if (allowDebug)
        {
            print("Powerup or Weapon is instantiated!");
        }
    }
}
