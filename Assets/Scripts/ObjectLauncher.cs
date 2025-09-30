using UnityEngine;

public class ObjectLauncher : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private Rigidbody launchedObjectPrefab;

    [Header("Timer")]
    [SerializeField] private bool activateViaTimer;
    [SerializeField] private float shootTime;
    private float shootTimer;

    [Header("Stats")]
    [SerializeField] private Vector3 initialVelocity;

    private void FixedUpdate()
    {
        if (!activateViaTimer) return;

        if (shootTimer <= 0) 
        {
            LaunchObject(); 
            shootTimer = shootTime;
        }
        else
        {
            shootTimer -= Time.fixedDeltaTime;
        }
    }

    [ContextMenu("Launch")]
    private void LaunchObject()
    {
        if (launchedObjectPrefab == null) Debug.LogError("Object to launch not set in inspector");

        Rigidbody laucnhedObject = Instantiate(launchedObjectPrefab, transform.position, Quaternion.identity, transform);
        laucnhedObject.AddForce(initialVelocity, ForceMode.VelocityChange);
    }
}
