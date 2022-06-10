using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // singleton
    // ENCAPSULATION
    public static GameManager _Instance {
        get;
        private set;
    }

    // control Variables;

    private float survivalTime;
    private bool startCounting;

    private void Awake()
    {
        if (_Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _Instance = this;
        startCounting = false;
    }

    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        if (startCounting)
        {
            survivalTime += Time.deltaTime;
        }
    }


    private void StartGame()
    {
        startCounting = true;
        survivalTime = 0;
        SpawnManager.Instance.StartSpawning();
    }

    public void GameOver()
    {
        SpawnManager.Instance.StopSpawning();

        // TODO : add a game over screen
    }

}
