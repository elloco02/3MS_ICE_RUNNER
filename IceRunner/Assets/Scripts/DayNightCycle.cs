using UnityEngine;
using UnityEngine.SceneManagement;

public class DayNightCycle : MonoBehaviour
{
    public Transform directionalLight; 
    public float cycleDurationInSeconds = 30;
    
    private bool _dayRunning = false;
    private float _currentRotation = 0f;
    private float _rotationSpeed;

    void Start()
    {
        StartNewDay();
        if (directionalLight == null)
        {
            var childLight = transform.Find("Directional Light");
            if (childLight != null)
            {
                directionalLight = childLight.GetComponent<Transform>();
            }

            if (directionalLight == null)
            {
                Debug.LogError("Directional light not found.");
                return;
            }
        }        
        if (directionalLight == null)
        {
            Debug.LogError("Directional Light ist nicht zugewiesen.");
            return;
        }

    }

    void Update()
    {
        if (!_dayRunning) return;
        
        float rotationPerSecond = 360f / cycleDurationInSeconds;
        directionalLight.Rotate(Vector3.right, rotationPerSecond * Time.deltaTime);
        
        _currentRotation += rotationPerSecond * Time.deltaTime;
        if (_currentRotation >= 180f)
        {
            EndDay();
        }
    }

    public void EndDay()
    {
        Debug.Log("Day Ended!");
        _dayRunning = false;
        SpawnManager.Instance.SpawnLastTile();
    }

    
    private void StartNewDay()
    {
        Debug.Log("New Day Started!");
        _dayRunning = true;
    }
    
    public bool IsDay()
    {
        return _dayRunning;
    }
}