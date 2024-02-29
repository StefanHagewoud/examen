using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Weapon : MonoBehaviour
{
    [SerializeField] private int maxMagAmmo;
    [SerializeField] private int magAmmo;
    [SerializeField] private bool infAmmo;
    [SerializeField] private int fireRate;
    [SerializeField] private int reloadTime;
    private float nextFireTime;
    [SerializeField] private float range;
    public float dmg;
    public GameObject rocket;

    // Start is called before the first frame update
    void Start()
    {
        magAmmo = maxMagAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime || Input.GetButton("Fire1") && fireRate == 0) {
            nextFireTime = Time.time + 1f / fireRate;
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            Reload();
        }

    }
    private void Shoot() {
        if (magAmmo > 0) {
            magAmmo--;
            RaycastHit hitInfo;
            if (Physics.Raycast(transform.parent.parent.position, transform.forward, out hitInfo, range)) {
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
