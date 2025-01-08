using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float currentMoveSpeed = 20f; // Startgeschwindigkeit
    public float maxMoveSpeed = 200;
    public int currentHighScore = 0;    // Aktueller Score
    private int savedHighScore = 0;

    private void Awake()
    {
        // Singleton sicherstellen
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Doppeltes Objekt zerstören
        }

        savedHighScore = PlayerPrefs.GetInt("HighScore", 0);
        Debug.Log($"Loaded high score: {savedHighScore}");
    }

    // Geschwindigkeit erhöhen (wird in der nächsten Szene verwendet)
    public void IncreaseSpeed(float increment)
    {
        if (currentMoveSpeed < maxMoveSpeed)
        {

            currentMoveSpeed += increment;
        }
    }

    // Score hinzufügen
    public void AddScore(int scoreFromRoundToAdd)
    {
        currentHighScore += scoreFromRoundToAdd;
        Debug.Log($"Current score added: {currentHighScore}");

        if (currentHighScore > savedHighScore)
        {
            savedHighScore = currentHighScore;

            PlayerPrefs.SetInt("HighScore", savedHighScore);
            PlayerPrefs.Save();
            Debug.Log($"New high score saved: {savedHighScore}");
        }
    }

    // Werte zurücksetzen (z. B. nach einem Spielende)
    public void ResetGame()
    {
        currentMoveSpeed = 20f;
        currentHighScore = 0;
    }

    public int GetSavedHighScore()
    {
        return savedHighScore;
    }

    public int GetCurrentHighScore()
    {
        return currentHighScore;
    }
}