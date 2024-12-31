using UnityEngine;
using UnityEngine.Serialization;

public class PlayerCollision : MonoBehaviour
{
    public DayNightCycle dayNightCycle;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Obstacle"))
        {
            return;
        }

        PlayerDied();
    }
    
    private void PlayerDied()
    {
        Debug.Log("Player Died!");
        dayNightCycle.EndDay();
        Destroy(gameObject);
        //TODO Game Over switch to death Screen
    }
}
