using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PickupManager : MonoBehaviour
{
    public GameObject gunHolderPrimary;
    public GameObject gunHolderSecondary;
    public S_Player playerScript;
    public bool enableCollision = true;
    public bool enableTrigger;
    public bool allowDestroyGameObjects = true;// Destroys GameObjects when they touch them.
    
    private void OnCollisionEnter(Collision col)
    {
        if (!enableCollision)
        {
            return;
        }

        else if (col.transform.CompareTag("Shield"))
        {
            PickupItem("Shield");
        }
        else if (col.transform.CompareTag("Med-Kit"))
        {
            PickupItem("Med-Kit");
        }
        else if (col.transform.CompareTag("RPG"))
        {
            PickupItem("RPG");
        }
        else if (col.transform.CompareTag("Weapon"))
        {
            PickupItem("Weapon");
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
            if (allowDestroyGameObjects)
            {
                DestroyCrateOrObject(other);
            }
            PickupItem("RPG");
        }
        else if (other.transform.CompareTag("Weapon"))
        {

            PickupItem("Weapon");
        }
    }

    public void PickupItem(string powerupName)
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

        }
        else if(powerupName == "Weapon")// Hier komt later code wanneer de wapen script af is.
        {

        }
    }
    private void DestroyCrateOrObject(Collider other)//Add Diagram.
    {
        Destroy(other.gameObject);
    }
}
