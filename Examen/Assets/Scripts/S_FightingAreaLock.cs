using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_FightingAreaLock : MonoBehaviour
{
    public List<BoxCollider> areaLockColliders;

    public void EnableAreaLock()
    {
        for (int i = 0; i < areaLockColliders.Count; i++)
        {
            areaLockColliders[i].enabled = true;
        }
    }
    public void DisableAreaLock()
    {
        for (int i = 0; i < areaLockColliders.Count; i++)
        {
            areaLockColliders[i].enabled = false;
        }
    }
}
