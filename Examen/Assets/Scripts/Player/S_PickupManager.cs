using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PickupManager : MonoBehaviour
{
    public GameObject gunHolderPrimary;
    public GameObject gunHolderSecondary;
    public S_Player playerScript;
    public bool enableCollision = true;//Add diagram
    public bool enableTrigger;//Add diagram
    public bool allowDestroyGameObjects = true;//Add diagram, Destroys GameObjects when they touch them.

    [Header("Scripts")]
    public S_WeaponSwitch weaponSwitch;//Add diagram
    
    private void OnCollisionEnter(Collision col)
    {
        if (!enableCollision)
        {
            return;
        }

        else if (col.transform.CompareTag("Shield"))
        {
            if (allowDestroyGameObjects)
            {
                DestroyCrateOrObject(col.collider);
            }
            PickupItem("Shield");
        }
        else if (col.transform.CompareTag("Med-Kit"))
        {
            if (allowDestroyGameObjects)
            {
                DestroyCrateOrObject(col.collider);
            }
            PickupItem("Med-Kit");
        }
        else if (col.transform.CompareTag("RPG"))
        {
            if (allowDestroyGameObjects)
            {
                DestroyCrateOrObject(col.collider);
            }
            PickupItem("RPG");
        }
        else if (col.transform.CompareTag("Weapon"))
        {
            GameObject weaponGameObject = col.gameObject.GetComponentInChildren<S_Weapon>().gameObject;
            PickupItem("Weapon", weaponGameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!enableTrigger)
        {
            return;
        }

        else if (other.transform.CompareTag("Armor"))
        {
            if (allowDestroyGameObjects)
            {
                DestroyCrateOrObject(other);
            }
            PickupItem("Armor");
        }
        else if (other.transform.CompareTag("Med-Kit"))
        {
            if (allowDestroyGameObjects)
            {
                DestroyCrateOrObject(other);
            }
            PickupItem("Med-Kit");
        }
        else if (other.transform.CompareTag("RPG"))
        {
            if (weaponSwitch.CheckWeaponSpace())
            {
                GameObject weaponGameObject = null;
                if (other.gameObject.GetComponentInChildren<S_Weapon>())
                {
                    weaponGameObject = other.gameObject.GetComponentInChildren<S_Weapon>().gameObject;
                }
                else if (other.gameObject.GetComponent<S_Weapon>())
                {
                    weaponGameObject = other.gameObject.GetComponent<S_Weapon>().gameObject;
                }
                PickupItem("RPG", weaponGameObject);
            }
        }
        else if (other.transform.CompareTag("Weapon"))
        {
            if (weaponSwitch.CheckWeaponSpace())
            {
                GameObject weaponGameObject = null;
                if (other.gameObject.GetComponentInChildren<S_Weapon>())
                {
                    weaponGameObject = other.gameObject.GetComponentInChildren<S_Weapon>().gameObject;
                }
                else if (other.gameObject.GetComponent<S_Weapon>())
                {
                    weaponGameObject = other.gameObject.GetComponent<S_Weapon>().gameObject;
                }
                PickupItem("Weapon", weaponGameObject);
            }
        }
    }

    public void PickupItem(string powerupName, GameObject weaponGameObject = null)
    {
        if(playerScript == null)
        {
            Debug.LogWarning("The script: S_Player. Is not attached to this variable.");
            return;
        }
        else if(powerupName == "Armor")
        {
            playerScript.armor += 1;
            playerScript.TakeDamage(0);// 0 damage zodat ik de UI kan refreshen.
        }
        else if(powerupName == "Med-Kit")
        {
            playerScript.health = playerScript.maxhealth;
            playerScript.TakeDamage(0);// 0 damage zodat ik de UI kan refreshen.
        }
        else if(powerupName == "RPG")// Hier komt later code wanneer de wapen script af is.
        {
            weaponSwitch.PickupGun(weaponGameObject);
        }
        else if(powerupName == "Weapon")// Hier komt later code wanneer de wapen script af is.
        {
            weaponSwitch.PickupGun(weaponGameObject);
        }
    }
    private void DestroyCrateOrObject(Collider other)//Add Diagram.
    {
        Destroy(other.gameObject);
    }
}
