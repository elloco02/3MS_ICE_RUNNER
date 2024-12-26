using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    //value sollte passend zum ziel score sein damit der tag rum ist, wenn das Ziel erreicht ist
    public float dayLength = 10f;

    private Transform _transform;

    void Start()
    {
        _transform = GetComponent<Transform>();
    }

    void Update()
    {
        _transform.Rotate(dayLength * Time.deltaTime, 0, 0);
    }
}