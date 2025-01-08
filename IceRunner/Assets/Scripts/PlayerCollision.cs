using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using TMPro;

public class PlayerCollision : MonoBehaviour
{
    public DayNightCycle dayNightCycle;
    public GameObject GameendScreen;
    [SerializeField] public TextMeshProUGUI gameendOverviewText;
    private bool _isInvincible = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Obstacle"))
        {
            return;
        }

        if (_isInvincible)
        {
            Debug.Log("Player is invincible, collision ignored!");
            return;
        }

        PlayerDied();
    }

    private void PlayerDied()
    {
        GameManager.Instance.IncreaseSpeed(20f);
        Debug.Log("Player Died!");
        dayNightCycle.EndDay();
        Destroy(gameObject);
        GameendScreen.SetActive(true);
        gameendOverviewText.text = $"Well Played! \n Your high score is {GameManager.Instance.GetSavedHighScore()}! \n You have collected {Collectable.Instance.GetCoinCount()} coins!";
    }

    public void MakeInvincible(float duration)
    {
        StartCoroutine(InvincibilityCoroutine(duration));
    }

    private IEnumerator InvincibilityCoroutine(float duration)
    {
        _isInvincible = true;
        Debug.Log("Player is now invincible!");

        yield return new WaitForSeconds(duration);

        _isInvincible = false;
        Debug.Log("Player is no longer invincible!");
    }
}
