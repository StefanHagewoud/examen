using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Weapon : MonoBehaviour
{
    private int maxMagAmmo;
    private int magAmmo;
    private bool infAmmo;
    private int fireRate;
    private int reloadTime;
    private float range;
    public float dmg;
    // Start is called before the first frame update
    void Start()
    {
        magAmmo = maxMagAmmo; // Initialize ammo in magazine
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }
    private void Shoot() {
        if (magAmmo > 0) {
            RaycastHit hitInfo;
            if (Physics.Raycast(transform.parent.parent.position, transform.forward, out hitInfo, range)) {
                hitInfo.transform.GetComponent<S_Enemy>().OnTakeDamage();
            }
            magAmmo--;
        } else {
            Reload();
        }
    }
    public void Reload() {
        if (!infAmmo && magAmmo < maxMagAmmo)
{

            StartCoroutine(ReloadDelay(reloadTime));
        }
    }

    IEnumerator ReloadDelay(float delay) {
        yield return new WaitForSeconds(delay);
        magAmmo = maxMagAmmo;
    }
}
