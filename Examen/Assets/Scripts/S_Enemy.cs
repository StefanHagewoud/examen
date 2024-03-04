using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class S_Enemy : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    public float health;
    [SerializeField]
    private float damage;
    [SerializeField]
    private float meleeRange;
    [SerializeField]
    private float stopRange;
    private NavMeshAgent enemyAgent;
    private Transform target;
    [SerializeField]
    private float attackdelay;
    private float attackRate;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private bool melee;

    void Start()
    {
        target = GameObject.Find("Player").transform;
        enemyAgent = GetComponent<NavMeshAgent>();
        enemyAgent.destination = target.position;
        if (!melee)
        {
            stopRange = Random.Range(4, 9);
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

            Vector3 eulerAngles = transform.eulerAngles;
            transform.eulerAngles = new Vector3(0f, eulerAngles.y, eulerAngles.z);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, meleeRange))
            {
                if (hit.collider.name == "Player")
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

            float rangeFromPlayer = Vector3.Distance(transform.position, target.position);
            if (rangeFromPlayer <= stopRange)
            {
                RaycastHit hit1;
                if (Physics.Raycast(transform.position, transform.forward, out hit1, 100f))
                {
                    if (hit.collider.name != "Player")
                    {
                        enemyAgent.isStopped = false;
                        Debug.DrawRay(transform.position, transform.forward * hit1.distance, Color.red);
                        //Debug.Log("Can not see player");
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
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * 700f);
            Destroy(bullet, 3f);
        }
    }
    public void Melee(float damage)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.GetChild(2).position, 5f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.name == "Player")
            {
                Debug.Log("MeleeAttack");
                //hitCollider.GetComponent<Player>().OntakeDamage(damage);
            }
        }
    }

    public void OnDeath()
    {
        //death animation 
        //death particles
        Destroy(gameObject);
    }
}
