using UnityEngine;

public class TriggerFloorGen : MonoBehaviour
{
    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        // Überprüfen, ob der Spieler den Trigger betritt
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;

            if (transform.parent.position == Vector3.zero) //Beim Trigger vom ersten Floor im Game
            {
                // Spawne n neue Floors, wenn der Spieler den ersten Trigger betritt
                SpawnManager.Instance.SpawnFirstNFloors(transform.parent);
            }
            else
            {
                // Spawne n neue Floors, wenn der Spieler den ersten Trigger betritt
                SpawnManager.Instance.SpawnNewFloor(transform.parent);
            }
        }
    }
}

