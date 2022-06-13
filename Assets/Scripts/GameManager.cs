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
    [SerializeField]private TextMeshProUGUI scoreText; 

    // control Variables;

    private float survivalTime;
    private bool startCounting;
    public bool isBossFight{ get; private set; }
    private int score;

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

    public void PlayerDied()
    {
        SpawnManager.Instance.StopSpawning();
        GameOverScreen.Instance.ActivateGameOverScreen(score, SpawnManager.Instance.GetWaveNumer());
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


    public void ResetCounter()
    {
        startCounting = false;
        int timePassed = Mathf.RoundToInt(survivalTime);
        score += (60 - timePassed > 0)? 60 - timePassed : 0; 
        survivalTime = 0;
        scoreText.text = "Score : " + score;
    }

    public void StartCounter()
    {
        survivalTime = 0;
        startCounting = true;
    }

}
