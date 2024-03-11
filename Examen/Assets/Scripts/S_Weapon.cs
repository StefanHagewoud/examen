using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Weapon : MonoBehaviour
{
    [Header("references")]
    public S_UiManager uiManager;
    public GameObject gunImpactEffect;
    public GameObject bulletPrefab;

    [Header("Gun Settings")]
    public int maxMagAmmo;
    public int magAmmo;
    [SerializeField] private bool infAmmo;
    [SerializeField] private float reloadTime;
    [SerializeField] private float range;
    public float fireRate;
    public bool automatic;
    public float dmg;

    //Private variables
    private float nextFireTime;
    private float delayTimer;
    [Header("Rocket Launcher Settings")]
    public GameObject rocketPrefab;
    private GameObject rocket;
    private bool firedRocket;
    private bool rocketExploding;


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
            Debug.Log("shot");
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
                            hitRB.AddExplosionForce(1000, rocket.transform.position, 10);
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
            if (transform.tag == "RPG") {
                rocket = Instantiate(rocketPrefab, transform.position, transform.rotation);
                firedRocket = true;
            } else if (transform.tag == "melee") {
                RaycastHit hitInfo;
                if (Physics.Raycast(transform.parent.parent.position, transform.forward, out hitInfo, range)) {
                    if (hitInfo.transform.tag == "Enemy") {
                        hitInfo.transform.GetComponent<S_Enemy>().TakeDamage(dmg);
                    }
                }
            } else {
                GameObject muscleFlash = Instantiate(transform.GetChild(0).gameObject, transform.GetChild(0).position, transform.rotation);
                Destroy(muscleFlash, 1f);
                GameObject bullet = Instantiate(bulletPrefab, transform.GetChild(1).position, transform.parent.rotation);
                bullet.GetComponent<S_Bullet>().damage = dmg;
                bullet.GetComponent<S_Bullet>().host = transform.parent.gameObject;
                bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 500f);
                Destroy(bullet, 3f);
                magAmmo--;
            }
        } else {
            Debug.Log("reloading");
            Reload();
        }
        uiManager.UpdateWeaponUI();
    }
    public void Reload() {
        if (magAmmo < maxMagAmmo) {
            StartCoroutine(ReloadDelay(reloadTime));
            uiManager.UpdateWeaponUI();
        }
    }

    IEnumerator ReloadDelay(float delay) {
        yield return new WaitForSeconds(delay);
        magAmmo = maxMagAmmo;
    }
}
