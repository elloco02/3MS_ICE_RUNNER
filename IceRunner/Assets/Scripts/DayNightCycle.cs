using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Transform directionalLight; 
    public float cycleDuration = 1f;
    public int pointsToCompleteDay = 100; 
    public bool startDay = false;

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
        if (!startDay) return;
        
        float adjustedSpeed = (cycleDuration / 30) * pointsToCompleteDay;
        directionalLight.transform.Rotate(Vector3.right, adjustedSpeed * Time.deltaTime);
    }

    public void EndDay()
    {
        Debug.Log("Day Ended!");
        startDay = false; 
    }

    public void StartNewDay()
    {
        Debug.Log("New Day Started!");
        startDay = true;
    }
}