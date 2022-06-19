using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    // singleton
    // ENCAPSULATION
    public static GameManager _Instance {
        get;
        private set;
    }


    // game objects
    [SerializeField] private GameObject bossRoom;
    [SerializeField] private GameObject mobsRoom;
    [SerializeField] private GameObject playerRef;
    // control Variables;

    private float survivalTime;
    private bool startCounting;
    public bool isBossFight{ get; private set; }
    private int score;
    public bool isWaveKilled {get; private set;}
    private bool isGameOver;
    private void Awake()
    {
        if (_Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        bossRoom.SetActive(false);
        mobsRoom.SetActive(true);
        _Instance = this;
        startCounting = false;
    }

    private void Start()
    {
        isGameOver = false;
        InGameUI.Instance.ToggleAdvanceText(false);
        StartGame();
    }

    private void Update()
    {
        if (isGameOver == false)
        {
            if (startCounting)
            {
                survivalTime += Time.deltaTime;
            }
        }
    }


    private void StartGame()
    {
        startCounting = true;
        survivalTime = 0;
        SpawnManager.Instance.StartSpawning();
    }

    public void PlayerDied()
    {
        isGameOver = true;
        InGameUI.Instance.ToggleAdvanceText(false);
        SpawnManager.Instance.StopSpawning();
        GameOverScreen.Instance.ActivateGameOverScreen(score, SpawnManager.Instance.GetWaveNumber() - 1);
        GameObject[] remainingEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in remainingEnemies)
        {
            Destroy(enemy);
        }
    }
    public void StartBossFight()
    {
        isBossFight = true;
    }

    public void EndBossFight()
    {
        isBossFight = false;
    }

    public void CheckEnemiesRemaining(int points)
    {
        UpdateScore(points);
        if (SpawnManager.Instance.AreAllEnemiesDead())
        {  
            InGameUI.Instance.ToggleAdvanceText(true);
            isWaveKilled = true;
            ResetCounter();
        }
    }
    
    public void UpdateScore(int points = 0, bool onlyMoney = false) /// updates score if points = 0 then level ended calculate time to add points
    {
        int amount = 0;
        if (!onlyMoney)
        {
            if (points > 0)
            {
                score += points;
                amount += points * 20;
            }
            else
            {
                int timePassed = Mathf.RoundToInt(survivalTime);
                score += (60 - timePassed > 0)? 60 - timePassed : 0;
                amount += score / 10; 
            }
        }
        int money = playerRef.GetComponent<PlayerController>().IncreaseMoney(amount);
        InGameUI.Instance.UpdateUI(score, money);
    }
    public void ResetCounter()
    {
        startCounting = false;
        UpdateScore();
        survivalTime = 0;
    }
    public void StartCounter()
    {
        survivalTime = 0;
        startCounting = true;
        isWaveKilled = false;
    }


    public void LoadNextArea()
    {
        if (SpawnManager.Instance.GetWaveNumber() % 5 == 0)
        {
            bossRoom.SetActive(true);
            mobsRoom.SetActive(false);
        }
        else
        {
            bossRoom.SetActive(false);
            mobsRoom.SetActive(true);
        }

        // destroying pick ups if the player leaves them
        GameObject[] pickups = GameObject.FindGameObjectsWithTag("PickUp");
        foreach (GameObject pickup in pickups)
        {
            if (pickup.GetComponent<PickUp>().onGround)
            {
                Destroy(pickup);
            }
        }
    }

    public void SpawnNextWave()
    {
            if (isWaveKilled)
            {
                InGameUI.Instance.ToggleAdvanceText(false);
                SpawnManager.Instance.StartSpawning();
            }
    }

}
