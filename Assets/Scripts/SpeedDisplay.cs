using TMPro;
using UnityEngine;

public class SpeedDisplay : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI speedTextBox;

    private Rigidbody trackedObject;

    private void OnEnable()
    {
        ObjectLauncher.OnObjectLaunched += ObjectLauncher_OnObjectLaunched;
    }

    private void OnDisable()
    {
        ObjectLauncher.OnObjectLaunched -= ObjectLauncher_OnObjectLaunched;
    }

    private void ObjectLauncher_OnObjectLaunched(Rigidbody obj)
    {
        trackedObject = obj;
    }

    private void FixedUpdate()
    {
        if (trackedObject != null) DisplaySpeed();
    }

    private void DisplaySpeed()
    {
        speedTextBox.text = "Speed: " + GetSpeedInMilesPerHour().ToString("0.0") + " MPH";
    }

    private float GetSpeedInMilesPerHour()
    {
        float speedInMetersPerSecond = trackedObject.linearVelocity.magnitude;
        float speedInMilesPerHour = speedInMetersPerSecond * 2.23694f;
        return speedInMilesPerHour;
    }
}
