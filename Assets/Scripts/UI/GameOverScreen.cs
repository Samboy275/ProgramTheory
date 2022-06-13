using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class GameOverScreen : MonoBehaviour
{
    // singleton
    // ENCAPSULATION
    public static GameOverScreen Instance{ get; private set; }
    [SerializeField]private GameObject gameOverScreen;
    [SerializeField]private Button restart;
    [SerializeField]private Button gameOver;
    [SerializeField]private TextMeshProUGUI endText;
    private string message;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance =  this;
        gameOverScreen.SetActive(false);
        restart.onClick.AddListener(Restart);
        gameOver.onClick.AddListener(MainMenu);
    }


    private void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void ActivateGameOverScreen(int score, int waves)
    {
        gameOverScreen.SetActive(true);

        if (MainManager._Instance.Save(score, waves))
        {
            message = "New High Score!!!";
        }
        else
        {
            message = "Better Luck Next Time";
        }
        endText.text = message;    
        }
}
