using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlowDownAndSwitchScene : MonoBehaviour
{
    private float slowDownDuration = 2f;
    private float maxSlowDownFactor = 0.1f;
    private float slowdownRate = 0.8f;

    private bool isTriggered = false;
    private float slowDownTimer = 0f;

    private void OnTriggerEnter(Collider other)
    {
        if (isTriggered || !other.CompareTag("Player"))
        {
            return;
        }

        isTriggered = true;
        StartCoroutine(SlowDownAndSwitchSceneCoroutine());
    }

    private IEnumerator SlowDownAndSwitchSceneCoroutine()
    {
        while (slowDownTimer < slowDownDuration)
        {
            slowDownTimer += Time.deltaTime;
            float slowFactor = Mathf.Lerp(1f, maxSlowDownFactor, slowDownTimer / slowDownDuration);
            Time.timeScale = slowFactor; 
            Time.fixedDeltaTime = 0.02f * Time.timeScale; 

            yield return null; 
        }

        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("Shop");
    }
}
