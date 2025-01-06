using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float currentMoveSpeed = 20f; // Startgeschwindigkeit
    public float maxMoveSpeed = 200;
    public int currentScore = 0;    // Aktueller Score

    private void Awake()
    {
        // Singleton sicherstellen
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Objekt über Szenen hinweg behalten
        }
        else
        {
            Destroy(gameObject); // Doppeltes Objekt zerstören
        }
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
    public void AddScore(int scoreToAdd)
    {
        currentScore += scoreToAdd;
    }

    // Werte zurücksetzen (z. B. nach einem Spielende)
    public void ResetGame()
    {
        currentMoveSpeed = 20f;
        currentScore = 0;
    }
}