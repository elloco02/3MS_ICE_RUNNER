using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;  // Singleton-Instanz

    public List<GameObject> floorPrefabs;       // Referenz auf das Floor-Prefab
    public Transform player;            // Referenz auf den Spieler

    private List<GameObject> activeFloors = new List<GameObject>(); // Liste der aktiven Floors
    public int maxFloors = 2;          // Anzahl der Floors, die gleichzeitig existieren sollen
    public float rotation = 10f;


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

    public void SpawnNewFloor(Transform currentFloor)
    {
        var randomTileFromList = RandomFloorFromList();
        
        // Berechne die Position und Rotation des neuen Floors
        Vector3 newPosition = currentFloor.position + currentFloor.forward * currentFloor.localScale.z;
        Quaternion newRotation = currentFloor.rotation;

        // Erstelle den neuen Floor
        // Erstelle den neuen Floor und füge ihn zur Liste hinzu
        GameObject newFloor = Instantiate(randomTileFromList, newPosition, newRotation);
        activeFloors.Add(newFloor);

        // Aktualisiere die Spielerrotation
        //UpdatePlayerRotation(rotation);

        // Wenn die Anzahl der Floors das Maximum überschreitet, lösche den ältesten Floor
        if (activeFloors.Count > maxFloors)
        {
            GameObject oldFloor = activeFloors[0]; // Ältester Floor
            activeFloors.RemoveAt(0);
            Destroy(oldFloor);
        }
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

}
