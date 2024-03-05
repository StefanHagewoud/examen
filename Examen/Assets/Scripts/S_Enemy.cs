using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class S_Enemy : MonoBehaviour
{
    private float moveSpeed;
    public float health;
    [SerializeField]
    private float damage;
    [SerializeField]
    private float range = 100f;
    private float stopRange;
    private NavMeshAgent enemyAgent;
    private Transform target;
    [SerializeField]
    private float attackdelay = 2f;
    private float attackRate;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private bool melee;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        enemyAgent = GetComponent<NavMeshAgent>();
        enemyAgent.destination = target.position;
        if (!melee)
        {
            stopRange = Random.Range(4, 10);
        }
    }

    void Update()
    {
        ChasePlayer();
    }

    public void ChasePlayer()
    {
        if(target != null)
        {
            //enemy Rotation
            transform.LookAt(target.transform);
            Vector3 eulerAngles = transform.eulerAngles;
            transform.eulerAngles = new Vector3(0f, eulerAngles.y, eulerAngles.z);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, range))
            {
                if(hit.collider!= null)
                {
                    if (hit.collider.transform.root.tag == "Player")
                    {
                        Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.yellow);

                        enemyAgent.isStopped = true;
                        if (attackRate <= 0f)
                        {
                            Attack(damage);
                            attackRate = attackdelay;
                        }
                        attackRate -= Time.deltaTime;
                    }
                }
            }

            float rangeFromPlayer = Vector3.Distance(transform.position, target.position);
            if (rangeFromPlayer <= stopRange)
            {
                RaycastHit hit1;
                if (Physics.Raycast(transform.position, transform.forward, out hit1, 100f))
                {
                    if(hit.collider != null)
                    {
                        if (hit.collider.transform.root.tag != "Player")
                        {
                            enemyAgent.isStopped = false;
                            Debug.DrawRay(transform.position, transform.forward * hit1.distance, Color.red);
                            //Debug.Log("Can not see player");
                        }
                    }
                }
            }
            else
            {
                enemyAgent.isStopped = false;
            }
            enemyAgent.destination = target.position;
        }
        else
        {
            enemyAgent.isStopped = true;
        }
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            OnDeath();
        }
    }

    public void Attack(float damage)
    {
        if (melee)
        {
            Melee(damage);
        }
        else
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.GetChild(2).position, transform.rotation);
            bullet.GetComponent<S_Bullet>().damage = damage;
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * 600f);
            Destroy(bullet, 3f);
        }
    }
    public void Melee(float damage)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.GetChild(2).position, 3f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.transform.tag == "Player")
            {
                Debug.Log("MeleeAttack");
                hitCollider.GetComponent<S_Player>().TakeDamage(damage);
            }
        }
    }

    public void OnDeath()
    {
        //death animation 
        //death particles
        GameObject.Find("WaveSpawner").GetComponent<S_WaveSpawner>().enemiesAlive--;
        Destroy(gameObject);
    }
}
