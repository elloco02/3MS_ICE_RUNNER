using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class MainMenu : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI highScoreText;
    [SerializeField] public TextMeshProUGUI totalCoinsText;

    private void Start()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = $"{highScore}";

        int totalCoins = PlayerPrefs.GetInt("CoinCount", 0);
        totalCoinsText.text = $"{totalCoins}";
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Level");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
