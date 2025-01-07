using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject currentCoinCount;
    [SerializeField] public TextMeshProUGUI coinCount;


    public void Pause()
    {
        pauseMenu.SetActive(true);
        currentCoinCount.SetActive(true);
        Time.timeScale = 0;
        UpdateCoinDisplay();
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        currentCoinCount.SetActive(false);
        Time.timeScale = 1;
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
}
