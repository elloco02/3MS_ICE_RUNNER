using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerCollision : MonoBehaviour
{
    public DayNightCycle dayNightCycle;
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
        //TODO Game Over switch to death Screen
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
