using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_WaveSpawner : MonoBehaviour
{
    [SerializeField]
    private List<Transform> spawnPoints;
    public Wave[] waves;
    private int wavesCount = 0;
    [SerializeField]
    private float waveInterval;
    private float countdown = 0f;
    public int enemiesAlive;
    [SerializeField]
    private GameObject[] cutScenes;

    void Update()
    {
        if(enemiesAlive > 0)
        {
            return;

        }

        if (countdown <= 0)
        {
            if (wavesCount >= waves.Length)
            {
                if(enemiesAlive <= 0)
                {
                    foreach (GameObject cutscene in cutScenes)
                    {
                        cutscene.SetActive(true);
                    }
                    Debug.Log("Completed all waves");
                }

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
        Wave wave = waves[wavesCount];
        enemiesAlive = waves[wavesCount].enemiesPerWave;
        for (int i = 0; i < wave.enemiesPerWave; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(wave.timeBetweenSpawns);
        }

        wavesCount++;
    }

    public void SpawnEnemy()
    {
        int spawnNummer = Random.Range(0, spawnPoints.Count);
        int enemyNummer = Random.Range(0, waves[wavesCount].enemyType.Count);
        GameObject spawnedEnemy = Instantiate(waves[wavesCount].enemyType[enemyNummer], spawnPoints[spawnNummer].position, spawnPoints[spawnNummer].rotation);

    }

    [System.Serializable]
    public class Wave
    {
        public float timeBetweenSpawns;
        public List<GameObject> enemyType;
        public int enemiesPerWave;
    }
}
