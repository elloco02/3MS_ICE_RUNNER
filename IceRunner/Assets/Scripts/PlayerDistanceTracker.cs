using UnityEngine;

public class PlayerDistanceTracker  : MonoBehaviour
{
    private float _distanceTraveled = 0f;
    private Vector3 _previousLoc;

    public DayNightCycle dayNightCycle;
    
    private void FixedUpdate()
    {
        if (dayNightCycle.IsDay())
        {
            _distanceTraveled += Vector3.Distance(transform.position, _previousLoc);
            _previousLoc = transform.position;
        }
        else if (!dayNightCycle.IsDay())
        {
            Debug.Log("Der Spieler hat eine Strecke von " + _distanceTraveled + " Einheiten zurückgelegt.");
            
            //TODO save into file before scene switch and deleting value
            _distanceTraveled = 0f;
        }
    }
}