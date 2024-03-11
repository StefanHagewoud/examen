using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Player : MonoBehaviour
{
    public S_UiManager uiManager;
    public float armor;
    public int score;
    public float health = 3f;
    public float maxhealth;
    public float meleeDmg;
    private float meleeRange;

    public GameObject damageParticle;
    //private S_DifficultyManager difficultyManager

    private void Start() {
        //DontDestroyOnLoad(transform.gameObject);
        TakeDamage(0);
    }
    public void OnDeath() {
        Destroy(gameObject);
    }

    public void TakeDamage(float dmg) {
        if (armor > 0) {
            armor -= dmg;
            if (armor < 0) {
                health += armor; // Subtract the remaining damage from health
                armor = 0;
            }
        } else {
            Debug.Log("damage");
            GameObject bloodParticle = Instantiate(damageParticle, transform.position, Quaternion.identity);
            Destroy(bloodParticle, 0.5f);
            health -= dmg;
        }

        if(health <= 0) {
            OnDeath();
        }

        // Update heart icons based on current health
        for (int i = 0; i < uiManager.hearts.childCount; i++) {
            if (i < health) {
                uiManager.hearts.GetChild(i).gameObject.SetActive(true);
            } else {
                uiManager.hearts.GetChild(i).gameObject.SetActive(false);
            }

        }

        // Update armor icons based on current armor
        for (int i = 0; i < uiManager.armor.childCount; i++) {
            if (i < armor) {
                uiManager.armor.GetChild(i).gameObject.SetActive(true);
            } else {
                uiManager.armor.GetChild(i).gameObject.SetActive(false);
            }

        }
    }
}
