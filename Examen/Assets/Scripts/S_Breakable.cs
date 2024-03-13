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
        GameObject explosion = Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(explosion, 1f);

        Collider[] hits = Physics.OverlapSphere(transform.position, explosiveRange);
        foreach (Collider hit in hits)
        {
            Debug.Log(hit.name + " hit by explosion");
            
            if (hit.TryGetComponent<Rigidbody>(out Rigidbody hitRB))
            {
                hitRB.AddExplosionForce(150, transform.position, explosiveRange);
            }
            if (hit.GetComponent<S_Player>())
            {
                hit.GetComponent<S_Player>().TakeDamage(damage);
            }
            if (hit.GetComponent<S_Enemy>())
            {
                hit.GetComponent<S_Enemy>().TakeDamage(damage * 4);
            }
        }
        Destroy(gameObject);
    }
}
