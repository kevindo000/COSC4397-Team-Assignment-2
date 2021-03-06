using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING };
    
    [System.Serializable]
    public class Wave
    {
        public string name;
        public GameObject enemyPrefab;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnPoints;

    public int maxWaves = 5;

    public float timeBetweenWaves = 2f;
    private float waveCountdown;

    private float searchCountdown = 1f;
    GameManager gameManagerObject;

    private SpawnState state = SpawnState.COUNTING;

    private void Start()
    {
        if (spawnPoints.Length == 0)
        {
            //To create a spawnpoint, make an empty game object then drag it to the spawn point script as spawnpoint.
            Debug.LogError("No Spawn Points used.");
        }
        waveCountdown = timeBetweenWaves;
    }

    private void Update()
    {
        if (state == SpawnState.WAITING)
        {
            if (!EnemiesAlive())
            {
                //Wave completed
                //Begin a new round. This can be where you can check if player starts new wave by checking if they hit next wave
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }

        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }
    void WaveCompleted()
    {
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > maxWaves)
        {
            //All waves complete. Put on victory screen here or something.


        }
        else
        {
            nextWave++;
        }
    }
    bool EnemiesAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectsWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        state = SpawnState.SPAWNING;

        for (int i=0; i <_wave.count;i++)
        {
            SpawnEnemy(_wave.enemyPrefab);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy( GameObject enemyPrefab)
    {
        //Spawn enemy at random spawn point(s)
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject go = Instantiate(enemyPrefab, _sp.position, Quaternion.identity) as GameObject;
        go.transform.parent = transform;
    }


}
