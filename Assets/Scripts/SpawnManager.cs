using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] enemies;

    [SerializeField] private float spawnRate;

    private void Start()
    {
        StartSpawning();
    }

    // ABSTRACTION
    private void SpawnEnemies()
    {
        int enemyIndex = Random.Range(0, enemies.Length);
        int pointIndex = Random.Range(0, spawnPoints.Length);

        Instantiate(enemies[enemyIndex], spawnPoints[pointIndex].position, enemies[enemyIndex].transform.rotation);
    }


    public void StartSpawning()
    {
        InvokeRepeating("SpawnEnemies", 0, spawnRate);
    }

    public void StopSpawning()
    {
        CancelInvoke();
    }
}
