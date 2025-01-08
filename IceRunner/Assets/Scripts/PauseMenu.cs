using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject currentCoinCount;
    [SerializeField] GameObject highScoreGroup;
    [SerializeField] GameObject sessionCoinCount;
    [SerializeField] public TextMeshProUGUI coinCount;
    [SerializeField] public TextMeshProUGUI highScore;
    private bool isPaused = false;


    private void Update()
    {
        Debug.Log("Update method called");
        if (Input.GetKeyDown(KeyCode.Escape) && PlayerCollision.Instance.isAlive)
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        currentCoinCount.SetActive(true);
        highScoreGroup.SetActive(true);
        sessionCoinCount.SetActive(false);
        isPaused = true;
        UpdateCoinDisplay();
        UpdateHighScoreDisplay();
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        currentCoinCount.SetActive(false);
        highScoreGroup.SetActive(false);
        sessionCoinCount.SetActive(true);
        Time.timeScale = 1;
        isPaused = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void Home()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1;
    }

    private void UpdateCoinDisplay()
    {
        if (Collectable.Instance != null)
        {
            int coinCountValue = Collectable.Instance.GetCoinCount();
            Debug.Log($"Coin count: {coinCountValue}");
            coinCount.text = $"{coinCountValue}";
        }
        else
        {
            Debug.LogError("Collectable instance is null!");
        }
    }

    public void UpdateHighScoreDisplay()
    {
        if (GameManager.Instance != null)
        {
            int highScoreValue = GameManager.Instance.GetSavedHighScore();
            Debug.Log($"High score: {highScoreValue}");
            highScore.text = $"{highScoreValue}";
        }
        else
        {
            Debug.LogError("GameManager instance is null!");
        }
    }
}
