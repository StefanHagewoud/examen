using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Boobytrap : MonoBehaviour
{
    public float dmg;
    private int range;
    private float explosiveRange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            Collider[] hits = Physics.OverlapSphere(transform.position, 10);
            foreach (Collider hit in hits) {
                Debug.Log(hit.name + " hit by explosion");
                if (hit.TryGetComponent<Rigidbody>(out Rigidbody hitRB)) {
                    hitRB.AddExplosionForce(1000, transform.position, 10);
                    // force, position, radius
                }
                if (hit.tag == "Enemy") {
                    hit.GetComponent<S_Enemy>().TakeDamage(dmg);
                }
                if (hit.tag == "Player") {
                    hit.GetComponent<S_Player>().TakeDamage(dmg);
                }
            }
            Destroy(gameObject);
        }
    }
}
