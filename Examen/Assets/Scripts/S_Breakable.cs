using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Breakable : MonoBehaviour
{
    public float damage;
    private bool explosive;
    [SerializeField]
    private float explosiveRange;
    [SerializeField]
    private float health;
    [SerializeField]
    private GameObject explosionEffect;

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        if(health <= 0)
        {
            OnBreak();
        }
    }

    public void OnBreak()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.1f);

        foreach (Collider nearbyObject in colliders)
        {
            // Apply explosion force to rigidbodies
            if (nearbyObject.CompareTag("Enemy") || nearbyObject.CompareTag("Player"))
            {
                Collider[] hits = Physics.OverlapSphere(transform.position, explosiveRange);
                foreach (Collider hit in hits)
                {
                    Debug.Log(hit.name + " hit by explosion");
                    GameObject explosion = Instantiate(explosionEffect, transform.position, transform.rotation);
                    Destroy(explosion, 1f);
                    if (hit.TryGetComponent<Rigidbody>(out Rigidbody hitRB))
                    {
                        hitRB.AddExplosionForce(150, transform.position, explosiveRange);
                        // force, position, radius
                    }
                    if (hit.GetComponent<S_Player>())
                    {
                        Debug.Log(hit.name + "has been hit");
                        hit.GetComponent<S_Player>().TakeDamage(damage);
                    }
                    if (hit.GetComponent<S_Enemy>())
                    {
                        Debug.Log(hit.name + "has been hit");
                        hit.GetComponent<S_Enemy>().TakeDamage(damage * 4);
                    }
                }
                Destroy(gameObject,1f);
            }
        }
    }
}
