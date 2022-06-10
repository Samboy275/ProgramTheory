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


    // control variables
    [SerializeField] private float spawnRate;
    [SerializeField] private int difficulty;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        difficulty = 1;
    }

    // ABSTRACTION
    private void SpawnEnemies()
    {
        for (int i = 0; i < difficulty; i++)
        {
            int enemyIndex = Random.Range(0, enemies.Length);
            int pointIndex = Random.Range(0, spawnPoints.Length);

            Instantiate(enemies[enemyIndex], spawnPoints[pointIndex].position, enemies[enemyIndex].transform.rotation);
        }
    }


    public void StartSpawning()
    {
        InvokeRepeating("SpawnEnemies", 0, spawnRate);
    }
     // POLYMORPHISM
    public void ChangeDifficulty(int newDifficulty)
    {
        difficulty = newDifficulty;
    }

    public void ChangeDifficulty(int newDifficulty, float newSpawnRate)
    {
        difficulty = newDifficulty;
        spawnRate = newSpawnRate;
    }
    public void StopSpawning()
    {
        CancelInvoke();
    }
}
