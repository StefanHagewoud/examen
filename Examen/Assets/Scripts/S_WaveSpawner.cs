using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_WaveSpawner : MonoBehaviour
{
    [SerializeField]
    private List<Transform> spawnPoints;
    [SerializeField]
    private List<GameObject> enemyType;
    public Wave[] waves;
    private int waveCount = 0;
    [SerializeField]
    private float waveInterval;
    private float countdown = 0f;
    public int enemiesAlive;

    void Update()
    {
        if(enemiesAlive > 0)
        {
            return;
        }

        if (countdown <= 0)
        {
            if (waveCount >= waves.Length)
            {
                Debug.Log("Completed all waves");
                gameObject.SetActive(false);
                return;
            }
            StartCoroutine(SpawnWave());
            countdown = waveInterval;
            return;
        }

        countdown -= Time.deltaTime;
    }

    IEnumerator SpawnWave()
    {
        Wave wave = waves[waveCount];

        for (int i = 0; i < wave.enemiesPerWave; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(wave.delay);
        }

        waveCount++;
    }

    public void SpawnEnemy()
    {
        int spawnNummer = Random.Range(0, spawnPoints.Count);
        int enemyNummer = Random.Range(0, enemyType.Count);
        GameObject spawnedEnemy = Instantiate(enemyType[enemyNummer], spawnPoints[spawnNummer].position, spawnPoints[spawnNummer].rotation);
        enemiesAlive++;
    }

    [System.Serializable]
    public class Wave
    {
        public float delay;
        public int enemiesPerWave;
    }
}
