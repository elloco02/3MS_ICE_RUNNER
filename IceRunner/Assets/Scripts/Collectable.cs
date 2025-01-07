using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private Dictionary<string, int> collectibles = new Dictionary<string, int>();
    public List<string> validCollectibles = new List<string>();
    public static Collectable Instance { get; private set; }

    private void Start()
    {
        int savedCoins = PlayerPrefs.GetInt("CoinCount", 0);
        collectibles["Coin"] = savedCoins;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Multiple instances of Collectable found!");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Collectible"))
        {
            return;
        }

        string itemName = other.gameObject.name;
        if (validCollectibles.Contains(itemName))
        {
            AddCollectible(itemName);
            Debug.Log($"Eingesammelt: {itemName}. Aktuelle Anzahl: {collectibles[itemName]}");

            Destroy(other.gameObject);
        }
    }

    public void AddCollectible(string itemName, int amount = 1)
    {
        if (!validCollectibles.Contains(itemName))
        {
            Debug.LogWarning($"Item {itemName} ist kein gültiges Collectible!");
            return;
        }

        if (!collectibles.ContainsKey(itemName))
        {
            collectibles[itemName] = 0;
        }

        collectibles[itemName] += amount;

        if (itemName == "Coin")
        {
            PlayerPrefs.SetInt("CoinCount", collectibles[itemName]);
            PlayerPrefs.Save();
        }
    }

    public void RemoveCollectible(string itemName, int amount = 1)
    {
        if (collectibles.ContainsKey(itemName))
        {
            collectibles[itemName] -= amount;

            if (collectibles[itemName] < 0)
            {
                collectibles[itemName] = 0;
            }
        }
        else
        {
            Debug.LogWarning($"Keine Collectibles mit dem Namen {itemName} vorhanden, die entfernt werden könnten.");
        }
    }

    public void PrintCollectiblesCount()
    {
        Debug.Log("Aktuelle Collectibles:");
        foreach (var collectible in collectibles)
        {
            Debug.Log($"{collectible.Key}: {collectible.Value}");
        }
    }

    public int GetCoinCount()
    {
        if (collectibles.ContainsKey("Coin"))
        {
            return collectibles["Coin"];
        }
        return 0;
    }
}
