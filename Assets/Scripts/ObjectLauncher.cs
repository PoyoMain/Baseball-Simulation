using System;
using UnityEngine;

public class ObjectLauncher : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private Rigidbody launchedObjectPrefab;

    [Header("Stats")]
    [SerializeField] private Vector3 initialVelocity;
    [SerializeField] private Vector3 initialTorque;
    [SerializeField] private bool useForce;
    [SerializeField] private bool useTorque;

    public static event Action<Rigidbody> OnObjectLaunched;

    public void LaunchObject()
    {
        if (launchedObjectPrefab == null) Debug.LogError("Object to launch not set in inspector");

        Rigidbody launchedObject = Instantiate(launchedObjectPrefab, transform.position, Quaternion.identity, transform);
        
        if (useForce) launchedObject.AddForce(initialVelocity, ForceMode.VelocityChange);
        else launchedObject.linearVelocity = initialVelocity;

        if (useTorque) launchedObject.AddTorque(initialTorque, ForceMode.VelocityChange);
        else launchedObject.angularVelocity = initialTorque;

        OnObjectLaunched?.Invoke(launchedObject);
    }
}
