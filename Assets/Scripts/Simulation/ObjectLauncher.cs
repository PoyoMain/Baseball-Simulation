using System;
using UnityEngine;

public class ObjectLauncher : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private Rigidbody launchedObjectPrefab;

    [Header("Stats")]
    [SerializeField] private float initVelocityMPH;
    [SerializeField] private Vector3 direction;
    [SerializeField] private Vector3 initialTorque;
    [SerializeField] private bool useForce;
    [SerializeField] private bool useTorque;

    public static event Action<Rigidbody> OnObjectLaunched;

    public void LaunchObject()
    {
        if (launchedObjectPrefab == null) Debug.LogError("Object to launch not set in inspector");

        Rigidbody launchedObject = Instantiate(launchedObjectPrefab, transform.position, Quaternion.identity, transform);
        Vector3 forceVector = direction * MPHtoMetersPerSecond(initVelocityMPH);

        if (useForce) launchedObject.AddForce(forceVector, ForceMode.VelocityChange);
        else launchedObject.linearVelocity = forceVector;

        if (useTorque) launchedObject.AddTorque(initialTorque, ForceMode.VelocityChange);
        else launchedObject.angularVelocity = initialTorque;

        OnObjectLaunched?.Invoke(launchedObject);
    }

    private float MPHtoMetersPerSecond(float mphSpeed)
    {
        float conversionRate = 0.44704f;
        return mphSpeed * conversionRate;
    }
}
