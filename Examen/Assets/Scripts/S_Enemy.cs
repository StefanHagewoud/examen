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
    private float meleeRange;
    [SerializeField]
    private float stopRange;
    [SerializeField]
    NavMeshAgent enemyAgent;
    public Transform target;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float fireCountDown;
    [SerializeField]
    private bool melee;

    void Start()
    {
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
        //enemy Rotation
        transform.LookAt(target.transform);
        Vector3 eulerAngles = transform.eulerAngles;
        transform.eulerAngles = new Vector3(0f, eulerAngles.y, eulerAngles.z);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, meleeRange))
        {
            if (hit.collider.name == "Player")
            {
                Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.yellow);

                float fireRate = 1;
                enemyAgent.isStopped = true;
                if (fireCountDown <= 0f)
                {
                    Attack();
                    fireCountDown = 2 / fireRate;
                }
                fireCountDown -= Time.deltaTime;
            }
        }

        float rangeFromPlayer = Vector3.Distance(transform.position, target.position);
        if (rangeFromPlayer <= stopRange)
        {
            RaycastHit hit1;
            if (Physics.Raycast(transform.position, transform.forward, out hit1, 100f))
            {
                if(hit.collider.name != "Player")
                {
                    //range = 3f;
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
        if (!melee)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.GetChild(2).position, transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * 700f);
            Destroy(bullet, 3f);
        }
        else
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.GetChild(2).position,1f);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.name == "Player")
                {
                    Debug.Log(hitCollider.name);
                    //hitCollider.GetComponent<Player>().OntakeDamage(damage);
                }
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
