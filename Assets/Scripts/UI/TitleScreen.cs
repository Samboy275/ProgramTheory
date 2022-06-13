using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif
public class TitleScreen : MonoBehaviour
{
    // UI elements
    [SerializeField] private Button StartGameBtn;
    [SerializeField] private Button ExitBtn;

    [SerializeField] private TMP_InputField PlayerName;

    [SerializeField] private TextMeshProUGUI highScoreText;


    private void Awake()
    {
        StartGameBtn.onClick.AddListener(StartGame);
        ExitBtn.onClick.AddListener(ExitGame);
    }

    private void Start()
    {
        int highScore = MainManager._Instance.LoadData();
        if (highScore > 0)
        {
            highScoreText.text = "Highest Score is " +  highScore + " by " + MainManager._Instance.GetName();
        }
    }
    private void StartGame()
    {
        if (string.IsNullOrWhiteSpace(PlayerName.text))
        {
            // TODO : Add an alert to make the player input name
            return;
        }

        // TODO : add a way to save the players name
        Debug.Log(PlayerName.text);
        MainManager._Instance.SetPlayerName(PlayerName.text);
        SceneManager.LoadScene(1);
    }



    private void ExitGame()
    {
        # if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        # else
            Application.Quit();
        # endif
    }
}
