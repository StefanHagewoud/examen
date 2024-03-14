using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Bullet : MonoBehaviour
{
    public float damage;
    [SerializeField]
    private GameObject hitParticle;
    public GameObject host;
    //destroys the bullet on impact & does damage if the player is hit.
    private void OnTriggerEnter(Collider other)
    {
        if(host.transform.root.tag != other.transform.root.tag && other.tag != "PickupManager")
        {
            Debug.Log("hit: " + other.gameObject.name + "by: " + host.tag);
            if (other.GetComponentInParent<S_Player>() != null) 
            {
                other.GetComponentInParent<S_Player>().TakeDamage(damage);
                Destroy(gameObject);
            } 
            else if (other.GetComponentInParent<S_Enemy>() != null) 
            {
                other.GetComponentInParent<S_Enemy>().TakeDamage(damage);
                Destroy(gameObject);
            } 
            else if(other.GetComponentInParent<S_Breakable>() != null)
            {
                other.GetComponentInParent<S_Breakable>().TakeDamage(damage);
                Destroy(gameObject);
            }
            else
            {
                GameObject hitEffect = Instantiate(hitParticle, transform.position, Quaternion.identity);
                Destroy(hitEffect, 1f);
                Destroy(gameObject);
            }
        }
    }
}
