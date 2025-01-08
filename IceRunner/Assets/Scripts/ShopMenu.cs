using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ShopMenu : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI coinsText;

    private void Start()
    {
        var button = GetComponent<Button>();
        int totalCoins = PlayerPrefs.GetInt("CoinCount", 0);
        coinsText.text = $"{totalCoins}";
        button.onClick.AddListener(PlayGame);
    }

    private void PlayGame()
    {
        SceneManager.LoadSceneAsync("Level");
    }
}
