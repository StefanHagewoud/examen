using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Bullet : MonoBehaviour
{
    public float damage;
    //destroys bullet if it hits anything but the Enemy
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.root.tag != "Enemy")
        {
            if(other.transform.root.tag == "Player")
            {
                other.GetComponent<S_Player>().TakeDamage(damage);
            }
            Destroy(this.gameObject);
        }
    }
}
