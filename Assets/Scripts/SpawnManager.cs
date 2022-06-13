using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // singleton 
    public static SpawnManager Instance { get; private set; }
    // gameobjects
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private GameObject boss;

    // control variables
    public int waveNumber {get; private set;}
    private int aliveEnemies;
    private void Awake()
    {
        aliveEnemies = 1;
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        waveNumber = 1;
    }

    // ABSTRACTION
    public void SpawnEnemies(int enemiesToSpawn)
    {
        if (enemiesToSpawn <= 0)
            return;
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int enemyIndex = Random.Range(0, enemies.Length);
            int pointIndex = Random.Range(0, spawnPoints.Length);

            Instantiate(enemies[enemyIndex], spawnPoints[pointIndex].position, enemies[enemyIndex].transform.rotation);
        }
        aliveEnemies = enemiesToSpawn;
    }

     // POLYMORPHISM
    // public void ChangeDifficulty(int newDifficulty)
    // {
    //     waveNumber = newDifficulty;
    // }

    // public void ChangeDifficulty(int newDifficulty, float newSpawnRate)
    // {
    //     waveNumber = newDifficulty;
    //     spawnRate = newSpawnRate;
    // }

    public void StartSpawning()
    {
        SpawnEnemies(waveNumber++);
    }
    public void StopSpawning()
    {
        CancelInvoke();
    }

    private void SpawnBoss()
    {
        aliveEnemies++;
        waveNumber++;
        Instantiate(boss, spawnPoints[0].position, boss.transform.rotation);
    }
    public void CheckEnemiesRemaining()
    {
        aliveEnemies--;
        if (aliveEnemies <= 0)
        {
            GameManager._Instance.ResetCounter();
            if (waveNumber % 5 == 0)
            {
                GameManager._Instance.StartBossFight();
                SpawnBoss();
                // mobs with boss logic
                int mobs = (waveNumber / 5) - 1;
                SpawnEnemies(mobs);
            }
            else
            {
                SpawnEnemies(waveNumber++);
            }
            GameManager._Instance.StartCounter();
        }
    }

    public int GetWaveNumer()
    {
        return waveNumber - 1;
    }
}
