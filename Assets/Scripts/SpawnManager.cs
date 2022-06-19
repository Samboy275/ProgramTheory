using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // singleton
    // ENCAPSULATION 
    public static SpawnManager Instance { get; private set; }
    // gameobjects
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject hpPickUp;
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
        
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int enemyIndex = Random.Range(0, enemies.Length);
            if (spawnIndex >= spawnPoints.Length)
            {
                spawnIndex = Random.Range(0, spawnPoints.Length);
            }
            Instantiate(enemies[enemyIndex], spawnPoints[spawnIndex++].position, enemies[enemyIndex].transform.rotation);
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
    public void StopSpawning()
    {
        CancelInvoke();
    }

    public void SpawnBoss()
    {
        aliveEnemies++;
        waveNumber++;
        Instantiate(boss, spawnPoints[0].position, boss.transform.rotation);
    }

    public bool AreAllEnemiesDead() // when an enemy killed decrease enemies returns true if all enemies are dead
    {
        aliveEnemies--;
        if (aliveEnemies == 0)
        {
            return true;
        }
        return false;
    }

    public int GetWaveNumber()
    {
        return waveNumber;
    }
    public void SpawnHpPickUp(Vector3 position)
    {
        int heartFallChance = Random.Range(1,4);

        if (heartFallChance % 3 == 0)
            Instantiate(hpPickUp, position, hpPickUp.transform.rotation);
    }

    // POLYMORPHISM
    public void SpawnHpPickUp(Vector3 position, int hp = 10)
    {
        GameObject hpRef = Instantiate(hpPickUp, position, hpPickUp.transform.rotation);
        hpRef.GetComponent<HealthPickup>().SetHpAmount(hp);
    }
}
