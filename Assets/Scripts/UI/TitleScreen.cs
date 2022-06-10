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



    private void Awake()
    {
        StartGameBtn.onClick.AddListener(StartGame);
        ExitBtn.onClick.AddListener(ExitGame);
    }

    private void StartGame()
    {
        if (string.IsNullOrWhiteSpace(PlayerName.text))
        {
            // TODO : Add an alert to make the player input name
            return;
        }

        // TODO : add a way to save the players name
        MainManager._Instance.SetPlayerName(name);
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
