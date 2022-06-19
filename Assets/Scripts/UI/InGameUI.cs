using UnityEngine;
using TMPro;

public class InGameUI : MonoBehaviour
{
    // singleton
    public static InGameUI Instance {get; private set;}
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI advanceText;
    [SerializeField] private TextMeshProUGUI moneyText;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    /// updates score and current money the player has
    public void UpdateUI(int score, int money)
    {
        scoreText.text = "Score : " + score;
        moneyText.text = money + "$"; 
    }


    /// toggles the advance text on/off
    public void ToggleAdvanceText(bool isActivate)
    {
        advanceText.gameObject.SetActive(isActivate);
    }
}
