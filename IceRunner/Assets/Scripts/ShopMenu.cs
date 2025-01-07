using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    private void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(PlayGame);
    }

    private void PlayGame()
    {
        SceneManager.LoadSceneAsync("Level");
    }
}
