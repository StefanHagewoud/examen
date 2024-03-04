using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;

public class S_CrateSpawner : MonoBehaviour
{
    public GameObject cratePrefab;
    public List<Transform> crateSpawnLocations;
    public Vector3 maximumSpawnRange;//Add Diagram

    [Header("Debug")]
    public bool allowDebug;

    public void SpawnCrate(int listNumber)
    {


        Instantiate(cratePrefab, maximumSpawnRange, Quaternion.identity, crateSpawnLocations[listNumber]);
    }
    private void OnDrawGizmos()
    {
        if (allowDebug)
        {
            Gizmos.DrawWireCube(transform.position, maximumSpawnRange);
        }
    }
}
