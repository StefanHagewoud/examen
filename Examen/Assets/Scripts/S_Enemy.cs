using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class S_Enemy : MonoBehaviour
{
    public float health;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float waitRange;
    public Transform target;
    [SerializeField]
    NavMeshAgent enemyAgent;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float bulletSpeed;

    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        enemyAgent.destination = target.position;
    }

    void Update()
    {
        ChasePlayer();
    }

    public void ChasePlayer()
    {
        transform.LookAt(target.transform);
        Vector3 eulerAngles = transform.eulerAngles;
        transform.eulerAngles = new Vector3(0f, eulerAngles.y, eulerAngles.z);

        float rangeFromPlayer = Vector3.Distance(transform.position, target.position);
        if (rangeFromPlayer <= waitRange)
        {
            enemyAgent.isStopped = true;
            Attack();
        }
        else
        {
            enemyAgent.isStopped = false;
        }
        enemyAgent.destination = target.position;
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            OnDeath();
        }
    }

    public void Attack()
    {
        Debug.Log("attack");
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * bulletSpeed);

    }

    public void OnDeath()
    {
        //death animation 
        //death particles
        Destroy(gameObject);
    }
}
