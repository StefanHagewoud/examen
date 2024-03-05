using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_WaveSpawner : MonoBehaviour
{
    [SerializeField]
    private List<Transform> spawnPoints;
    [SerializeField]
    private List<GameObject> enemyType;
    [SerializeField]
    private int enemiesPerWave;
    [SerializeField]
    private int waves;
    [SerializeField]
    private float timeBetweenSpawns;
    [SerializeField]
    private float waveInterval;
    [SerializeField]
    private int enemies;
    public int maxWaves;

    void Update()
    {
        if(waves == maxWaves)
        {
            return;
        }

        if(enemies != enemiesPerWave)
        {
            if (timeBetweenSpawns <= 0f)
            {
                StartCoroutine(SpawnWave());
                timeBetweenSpawns = 1;
            }
            timeBetweenSpawns -= Time.deltaTime;
        }
        else
        {
            waveInterval -= Time.deltaTime;
            if(waveInterval < 0)
            {
                waveInterval = 10f;
                enemies = 0;
                waves++;
            }
        }
    }

    IEnumerator SpawnWave()
    {
        SpawnEnemy();
        yield return new WaitForSeconds(timeBetweenSpawns);
        //waves++;
    }

    public void SpawnEnemy()
    {
        enemies++;
        int spawnNummer = Random.Range(0, spawnPoints.Count);
        int enemyNummer = Random.Range(0, enemyType.Count);
        GameObject spawnedEnemy = Instantiate(enemyType[enemyNummer], spawnPoints[spawnNummer].position, spawnPoints[spawnNummer].rotation);
    }
}
