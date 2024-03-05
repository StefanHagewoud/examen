using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Weapon : MonoBehaviour
{
    [SerializeField] private int maxMagAmmo;
    [SerializeField] private int magAmmo;
    [SerializeField] private bool infAmmo;
    public int fireRate;
    [SerializeField] private int reloadTime;
    private float nextFireTime;
    [SerializeField] private float range;
    public float dmg;
    public GameObject rocket;
    public GameObject rocketPrefab;
    private bool firedRocket;
    private bool rocketExploding;
    private float delayTimer;

    // Start is called before the first frame update
    void Start()
    {
        magAmmo = maxMagAmmo;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime || Input.GetButton("Fire1") && fireRate == 0) {
            nextFireTime = Time.time + 1f / fireRate;
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            Reload();
        }


        if (firedRocket) {
            if (!rocketExploding) {
                rocket.GetComponent<Rigidbody>().velocity = rocket.transform.forward * 10;
            } else {
                rocket.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
            delayTimer += Time.deltaTime;

            Collider[] colliders = Physics.OverlapSphere(rocket.transform.position, 0.1f);

            foreach (Collider nearbyObject in colliders) {
                // Apply explosion force to rigidbodies
                if (!nearbyObject.CompareTag("RPG") && !nearbyObject.CompareTag("Player")) {
                    rocketExploding = true;
                    //GameObject.Instantiate(currentGunData.rocketExplodeAnim, rocket.transform.position, rocket.transform.rotation);
                    Collider[] hits = Physics.OverlapSphere(rocket.transform.position, 10);
                    foreach (Collider hit in hits) {
                        Debug.Log(hit.name + " hit by explosion");
                        if (hit.TryGetComponent<Rigidbody>(out Rigidbody hitRB)) {
                            hitRB.AddExplosionForce(100, rocket.transform.position, 10);
                            // force, position, radius
                        }
                        if (hit.GetComponent<S_Enemy>()) {
                            Debug.Log(hit.name + "has been hit");
                            hit.GetComponent<S_Enemy>().TakeDamage(dmg);
                        }
                    }
                    Destroy(rocket);
                    firedRocket = false;
                    rocketExploding = false;
                }
            }
            if (delayTimer >= 5) {
                Destroy(rocket);
                firedRocket = false;
                rocketExploding = false;
                delayTimer = 0f;
            }
        }
    }
    public void Shoot() {
        if (magAmmo > 0) {
            magAmmo--;
            RaycastHit hitInfo;
            if (transform.tag == "RPG") {
                rocket = Instantiate(rocketPrefab, transform.position, transform.rotation);
                firedRocket = true;
            } else if (Physics.Raycast(transform.parent.parent.position, transform.forward, out hitInfo, range)) {
                hitInfo.transform.GetComponent<S_Enemy>().TakeDamage(dmg);
            }
        } else {
            Reload();
        }
    }
    public void Reload() {
        if (!infAmmo && magAmmo < maxMagAmmo) {
            StartCoroutine(ReloadDelay(reloadTime));
        }
    }

    IEnumerator ReloadDelay(float delay) {
        yield return new WaitForSeconds(delay);
        magAmmo = maxMagAmmo;
    }
}
