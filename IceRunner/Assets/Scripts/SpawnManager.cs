using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;  // Singleton-Instanz

    public List<GameObject> floorPrefabs;       // Referenz auf das Floor-Prefab
    public GameObject lastTile;
    public Transform player;            // Referenz auf den Spieler
    private List<GameObject> activeFloors = new List<GameObject>(); // Liste der aktiven Floors
    public int firstNFloors = 3;          // Anzahl der Floors, die gleichzeitig existieren sollen
    public float rotation = 10f;

    private bool _roundOver = false;
    
    private void Awake()
    {
        // Singleton einrichten
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        // Initiale Floors generieren (z. B. erster Floor + ein weiterer)
        Quaternion initialRotation = Quaternion.Euler(rotation, 0f, 0f);
        // Aktualisiere die Spielerrotation
        UpdatePlayerRotation(rotation);
        var randomTileFromList = RandomFloorFromList();
        GameObject firstFloor = Instantiate(randomTileFromList, Vector3.zero, initialRotation);
        activeFloors.Add(firstFloor);
    }



    public void SpawnFirstNFloors(Transform currentFloor)
    {
        destroyOldestTile(currentFloor); // Löscht immer den letzten Floor hinter dem Player
        Transform lastFloor = currentFloor; // Start mit dem aktuellen Boden
        for (int i = 0; i < firstNFloors; i++)
        {
            // Wähle eine zufällige Tile aus der Liste
            var randomTileFromList = RandomFloorFromList();

            // Berechne die Position und Rotation für den neuen Boden
            Vector3 newPosition = calculateNextFloorPosition(lastFloor);
            Quaternion newRotation = lastFloor.rotation;

            // Erstelle den neuen Boden
            GameObject newFloor = Instantiate(randomTileFromList, newPosition, newRotation);
            activeFloors.Add(newFloor);

            // Setze den zuletzt erstellten Boden als Referenz für den nächsten
            lastFloor = newFloor.transform;
        }
    }

    public void SpawnNewFloor(Transform currentFloor)
    {
        destroyOldestTile(currentFloor);
        
        var randomTileFromList = RandomFloorFromList();

        if (_roundOver)
        {
            randomTileFromList = lastTile;
        }
        
        Transform latestFloor = activeFloors[^1].transform;
        // Berechne die Position und Rotation des neuen Floors
        Vector3 newPosition = calculateNextFloorPosition(latestFloor);
        Quaternion newRotation = latestFloor.rotation;

        // Erstelle den neuen Floor
        // Erstelle den neuen Floor und füge ihn zur Liste hinzu
        GameObject newFloor = Instantiate(randomTileFromList, newPosition, newRotation);
        activeFloors.Add(newFloor);

       
        
        // Aktualisiere die Spielerrotation
        //UpdatePlayerRotation(rotation);

    }

    private void UpdatePlayerRotation(float floorRotation)
    {
        // Passe die Rotation des Spielers an
        Quaternion playerRotation = Quaternion.Euler(floorRotation, 0f, 0f);
        player.rotation = playerRotation;
    }

    private GameObject RandomFloorFromList()
    {
        return floorPrefabs[Random.Range(0, floorPrefabs.Count)];
    }

    private static Vector3 calculateNextFloorPosition(Transform referenceFloor){
        return referenceFloor.position + referenceFloor.forward * GetTileLength(referenceFloor.gameObject);
    }

    private void destroyOldestTile(Transform currentFloor)
    {
        if (activeFloors.Count > 1 && currentFloor.position != Vector3.zero)
        {
            GameObject oldFloor = activeFloors[0]; // Ältester Floor
            activeFloors.RemoveAt(0);
            Destroy(oldFloor);
        }
    }

    // Hilfsmethode, um die Länge eines Tiles zu ermitteln
    private static float GetTileLength(GameObject tile)
    {
        // Versuche zuerst, die Länge aus dem Collider zu berechnen
        BoxCollider collider = tile.GetComponent<BoxCollider>();
        if (collider != null)
        {
            return collider.size.z * tile.transform.localScale.z;
        }

        // Alternativ: Länge aus dem Renderer berechnen
        Renderer renderer = tile.GetComponent<Renderer>();
        if (renderer != null)
        {
            return renderer.bounds.size.z;
        }

        // Fallback: Standardwert für Tile Länge (falls keine Länge ermittelt werden kann)
        return 150f;
    }

    public void SpawnLastTile()
    {
        _roundOver = true;
    }
}
