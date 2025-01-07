using UnityEngine;

public class RandomYRotation : MonoBehaviour
{
    [SerializeField] private float minSpeed = 10f;
    [SerializeField] private float maxSpeed = 100f; 

    private float _rotationSpeed;

    private void Start()
    {
        _rotationSpeed = Random.Range(minSpeed, maxSpeed);

        var randomYRotation = Random.Range(0f, 360f); 
        transform.rotation = Quaternion.Euler(0, randomYRotation, 0);
    }

    private void Update()
    {
        transform.Rotate(0, _rotationSpeed * Time.deltaTime, 0);
    }
}