using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Bullet : MonoBehaviour
{
    public float damage;
    [SerializeField]
    private GameObject hitParticle;
    //destroys the bullet on impact & does damage if the player is hit.
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.root.tag != "Enemy")
        {
            if(other.transform.root.tag == "Player")
            {
                if(other.GetComponent<S_Player>() != null)
                {
                    other.GetComponent<S_Player>().TakeDamage(damage);
                }
            }
            else
            {
                GameObject hitEffect = Instantiate(hitParticle, transform.position, Quaternion.identity);
                Destroy(hitEffect, 1f);
            }
            Destroy(gameObject);
        }
    }
}
