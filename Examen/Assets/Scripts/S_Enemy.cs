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
    public bool revolver;
    [SerializeField]
    private bool boss;
    public bool passive;
    public bool rocketLauncher;
    [SerializeField]
    private GameObject bloodParticle;
    [SerializeField]
    private GameObject muzzleFlashParticle;
    [SerializeField]
    private GameObject explosionEffect;
    private float timer;
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject rpgModel;
    [SerializeField]
    private GameObject tommyModel;
    [SerializeField]
    private GameObject revolverModel;
    [SerializeField]
    private GameObject knuppelModel;

    [Header("Rocket Launcher Settings")]
    public GameObject rocketPrefab;
    private GameObject rocket;
    private bool firedRocket;
    private bool rocketExploding;

    void Start()
    {
        health = maxHealth;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        enemyAgent = GetComponent<NavMeshAgent>();
        if (enemyAgent.enabled)
        {
            enemyAgent.destination = target.position;
        }
        
        if (!boss)
        {
            if (revolver)
            {
                animator.SetLayerWeight(2, 1);
                revolverModel.SetActive(true);
            }
            else
            {
                animator.SetLayerWeight(3, 1);
                tommyModel.SetActive(true);
            }
            if (!melee)
            {
                stopRange = Random.Range(4, 10);
            }
            else
            {
                animator.SetLayerWeight(3, 0);
                tommyModel.SetActive(false);
                animator.SetLayerWeight(2, 0);
                revolverModel.SetActive(false);

                animator.SetLayerWeight(1, 1);
                knuppelModel.SetActive(true);
            }
        }
        else
        {
            animator.SetLayerWeight(4, 1);
            rpgModel.SetActive(true);
        }
    }

    public void TogglePassive(bool toggle)
    {
        passive = toggle;
    }

    void Update()
    {
        if (enemyAgent.enabled)
        {
            if (!enemyAgent.isStopped)
            {
                animator.SetFloat("Running", 1);
            }
            else
            {
                animator.SetFloat("Running", 0);
            }
        }

        ChasePlayer();
        if (rocketLauncher)
        {
            if (firedRocket)
            {
                if (!rocketExploding)
                {
                    rocket.GetComponent<Rigidbody>().velocity = rocket.transform.forward * 10;
                }
                else
                {
                    rocket.GetComponent<Rigidbody>().velocity = Vector3.zero;
                }
                timer += Time.deltaTime;

                Collider[] colliders = Physics.OverlapSphere(rocket.transform.position, 0.1f);

                foreach (Collider nearbyObject in colliders)
                {
                    // Apply explosion force to rigidbodies
                    if (!nearbyObject.CompareTag("Enemy"))
                    {
                        rocketExploding = true;
                        Collider[] hits = Physics.OverlapSphere(rocket.transform.position, 10);
                        foreach (Collider hit in hits)
                        {
                            Debug.Log(hit.name + " hit by explosion");
                            GameObject explosion = Instantiate(explosionEffect, rocket.transform.position, rocket.transform.rotation);
                            Destroy(explosion, 1f);
                            if (hit.TryGetComponent<Rigidbody>(out Rigidbody hitRB))
                            {
                                hitRB.AddExplosionForce(150, rocket.transform.position, 10);
                                // force, position, radius
                            }
                            if (hit.GetComponent<S_Player>())
                            {
                                Debug.Log(hit.name + "has been hit");
                                hit.GetComponent<S_Player>().TakeDamage(damage);
                            }
                        }
                        Destroy(rocket);
                        firedRocket = false;
                        rocketExploding = false;
                    }
                }
                if (timer >= 5)
                {
                    Destroy(rocket);
                    firedRocket = false;
                    rocketExploding = false;
                    timer = 0f;
                }
            }
        }
    }

    public void ChasePlayer()
    {
        if(target != null)
        {
            if (passive)
            {
                animator.SetFloat("Running", 0);
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
                    enemyAgent.isStopped = true;
                    if (boss)
                    {
                        if (attackRate <= 0f)
                        {
                            Attack(damage);
                            attackRate = attackdelay;
                        }
                        attackRate -= Time.deltaTime;
                    }
                   
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
                if (rocketLauncher && boss)
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
            if(enemyAgent != null)
            {
                enemyAgent.isStopped = true;
            }
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
            stopRange = 12f;
            rocketLauncher = false;
            attackRate = 0.5f;
            tommyModel.SetActive(true);
            rpgModel.SetActive(false);
            animator.SetLayerWeight(4, 0);
            animator.SetLayerWeight(3, 1);
        }
        if (health <= 0)
        {
            OnDeath();
        }
    }

    public void Attack(float damage)
    {
        if (rocketLauncher)
        {
            animator.SetTrigger("RPG_Shoot");
            firedRocket = true;
            rocket = Instantiate(rocketPrefab, transform.GetChild(2).position, transform.rotation);
            GameObject muzzleFlash = Instantiate(muzzleFlashParticle, transform.GetChild(2).position, transform.rotation);
            Destroy(muzzleFlash, 1f);
        }
        if (!rocketLauncher)
        {
            if (melee)
            {
                animator.SetTrigger("Knuppel_Swing");
                Melee(damage);
            }
            else
            {
                if (revolver)
                {
                    animator.SetTrigger("Revolver_Shoot");
                }
                else
                {
                    animator.SetTrigger("Tommy_Gun_Shoot");
                }

                GameObject muzzleFlash = Instantiate(muzzleFlashParticle, transform.GetChild(2).position, transform.rotation);
                Destroy(muzzleFlash, 1f);
                GameObject bullet = Instantiate(bulletPrefab, transform.GetChild(2).position, transform.rotation);
                bullet.GetComponent<S_Bullet>().damage = damage;
                bullet.GetComponent<S_Bullet>().host = gameObject;

                bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * 500f);

                Destroy(bullet, 3f);
            }
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
        animator.SetTrigger("Die");
        GameObject cutSceneObj = GameObject.Find("Cutscene 6.0");
        cutSceneObj.SetActive(true);

        if (GameObject.Find("WaveSpawner") != null)
        {
            GameObject.Find("WaveSpawner").GetComponent<S_WaveSpawner>().enemiesAlive--;
        }
        if(GameObject.Find("PF_ScoreManager") != null)
        {
            GameObject.Find("PF_ScoreManager").GetComponent<S_ScoreManager>().AddScore(scorePerEnemy);
        }
        passive = true;
        Destroy(gameObject,1f);
    }
}
