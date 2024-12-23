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

            // Spawne einen neuen Floor, wenn der Spieler den Trigger betritt
            SpawnManager.Instance.SpawnNewFloor(transform.parent);
        }
    }
}

