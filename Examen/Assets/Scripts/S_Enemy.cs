using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class S_Enemy : MonoBehaviour
{
    private float moveSpeed;
    public int scorePerEnemy;
    public float health;
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float damage;
    [SerializeField]
    private float range = 100f;
    [SerializeField]
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
    [SerializeField]
    private bool boss;
    public bool passive;
    [SerializeField]
    private GameObject bloodParticle;
    [SerializeField]
    private GameObject muzzleFlashParticle;

    void Start()
    {
        health = maxHealth;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        enemyAgent = GetComponent<NavMeshAgent>();
        enemyAgent.destination = target.position;
        if (!boss)
        {
            if (!melee)
            {
                stopRange = Random.Range(4, 10);
            }
        }
    }

    public void TogglePassive(bool toggle)
    {
        passive = toggle;
    }

    void Update()
    {
        if (boss && health <= maxHealth / 2)
        {
            enemyAgent.isStopped = false;
            stopRange = 12f;
        }
        ChasePlayer();
    }

    public void ChasePlayer()
    {
        if(target != null)
        {
            if (passive)
            {
                enemyAgent.enabled = false;
                return;
            }
            else
            {
                enemyAgent.enabled = true;
                //enemy Rotation
                transform.LookAt(target.transform);
                Vector3 eulerAngles = transform.eulerAngles;
                transform.eulerAngles = new Vector3(0f, eulerAngles.y, eulerAngles.z);
            }

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
                if (boss)
                {
                    enemyAgent.isStopped = true;
                }
                
                else
                {
                    RaycastHit hit1;
                    if (Physics.Raycast(transform.position, transform.forward, out hit1, 100f))
                    {
                        if (hit.collider != null)
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
        //hitParticles
        GameObject bloodEffect = Instantiate(bloodParticle, transform.position, Quaternion.identity);
        Destroy(bloodEffect, 1f);
        //hitanimation
        if(boss && health <= maxHealth / 2)
        {
            enemyAgent.isStopped = false;
            stopRange = 15f;
        }
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
            GameObject muzzleFlash = Instantiate(muzzleFlashParticle, transform.GetChild(2).position, transform.rotation);
            Destroy(muzzleFlash, 1f);
            GameObject bullet = Instantiate(bulletPrefab, transform.GetChild(2).position, transform.rotation);
            bullet.GetComponent<S_Bullet>().damage = damage;
            bullet.GetComponent<S_Bullet>().host = gameObject;
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * 500f);
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
        
        if (GameObject.Find("WaveSpawner") != null)
        {
            GameObject.Find("WaveSpawner").GetComponent<S_WaveSpawner>().enemiesAlive--;
        }
        if(GameObject.Find("PF_ScoreManager") != null)
        {
            GameObject.Find("PF_ScoreManager").GetComponent<S_ScoreManager>().AddScore(scorePerEnemy);
        }

        Destroy(gameObject);
    }
}
