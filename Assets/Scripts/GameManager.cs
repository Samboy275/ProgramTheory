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
    [SerializeField] private TextMeshProUGUI scoreText; 
    [SerializeField] private TextMeshProUGUI advanceText;
    [SerializeField] private GameObject bossRoom;
    [SerializeField] private GameObject mobsRoom;
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
        advanceText.gameObject.SetActive(false);
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
        advanceText.gameObject.SetActive(false);
        SpawnManager.Instance.StopSpawning();
        GameOverScreen.Instance.ActivateGameOverScreen(score, SpawnManager.Instance.GetWaveNumer() - 1);
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
        score += points;
        UpdatePoints();
        if (SpawnManager.Instance.AreAllEnemiesDead())
        {
                
            advanceText.gameObject.SetActive(true);
            isWaveKilled = true;
            ResetCounter();
        }
    }
    public void ResetCounter()
    {
        startCounting = false;
        int timePassed = Mathf.RoundToInt(survivalTime);
        score += (60 - timePassed > 0)? 60 - timePassed : 0; 
        survivalTime = 0;
        UpdatePoints();
    }

    private void UpdatePoints() => scoreText.text = "Points : " + score;
    public void StartCounter()
    {
        survivalTime = 0;
        startCounting = true;
        isWaveKilled = false;
    }


    public void LoadNextArea()
    {
        if (SpawnManager.Instance.GetWaveNumer() % 5 == 0)
        {
            bossRoom.SetActive(true);
            mobsRoom.SetActive(false);
        }
        else
        {
            bossRoom.SetActive(false);
            mobsRoom.SetActive(true);
        }

        // destorying pick ups if the player leaves them
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
                advanceText.gameObject.SetActive(false);
                SpawnManager.Instance.StartSpawning();
            }
    }

}
