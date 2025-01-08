using UnityEngine;
using TMPro;
using System.Collections;

public class PopupController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI popupText;
    public static PopupController Instance { get; private set; }
    [SerializeField] private float fadeDuration = 2f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("PopupController initialized successfully.");

        }
        else
        {
            Debug.LogError("Multiple instances of PopupController found!");
            Destroy(gameObject);
        }

        if (popupText == null)
        {
            Debug.LogError("popupText is not assigned in PopupController.");
        }
    }


    public void ShowPopup(string message)
    {
        popupText.text = message;
        StopAllCoroutines();
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        // Make the text fully visible
        popupText.gameObject.SetActive(true);

        // Wait for a moment before fading
        yield return new WaitForSeconds(1f);

        // Gradually fade out
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            popupText.alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Make the text invisible
        popupText.gameObject.SetActive(false);
    }
}
