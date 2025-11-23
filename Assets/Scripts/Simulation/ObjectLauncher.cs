using System;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

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

    private const string DISTANCE_KEY = "Distance";
    private const string MINIMAL_SPEED_KEY = "MinimalSpeed";
    private const string MAXIMAL_SPEED_KEY = "MaximalSpeed";
    private const string INCREMENT_KEY = "Increment";
    private const string RANDOMIZE_SPEED_KEY = "RandomizeSpeed";
    private const string SEAM_COUNT_KEY = "SeamCount";
    private const string SPIN_KEY = "Spin";

    public static UnityAction<Rigidbody> OnObjectLaunched;

    private SeamCount seamCount;
    private Spin spin;

    private void OnEnable()
    {
        SettingsUI.OnValuesUpdated += SetValues;
    }

    private void OnDisable()
    {
        SettingsUI.OnValuesUpdated -= SetValues;
    }

    private void Start()
    {
        SetValues();
    }

    private void SetValues()
    {
        if (PlayerPrefs.HasKey(DISTANCE_KEY))
        {
            Vector3 position = transform.position;
            position.x = FeetToMeters(PlayerPrefs.GetFloat(DISTANCE_KEY));
            transform.position = position;
        }

        int minSpeed = PlayerPrefs.GetInt(MINIMAL_SPEED_KEY);
        int maxSpeed = PlayerPrefs.GetInt(MAXIMAL_SPEED_KEY);

        if (PlayerPrefs.HasKey(RANDOMIZE_SPEED_KEY) && PlayerPrefs.GetInt(RANDOMIZE_SPEED_KEY) == 1)
        {
            initVelocityMPH = Random.Range(minSpeed, maxSpeed);
        }
        else
        {
            initVelocityMPH = Mathf.Clamp(initVelocityMPH, minSpeed, maxSpeed);
        }

        seamCount = (SeamCount)PlayerPrefs.GetInt(SEAM_COUNT_KEY);
        spin = (Spin)PlayerPrefs.GetInt(SPIN_KEY);
    }

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

    private float FeetToMeters(float feetValue)
    {
        float conversionRate = 0.3048f;
        return feetValue * conversionRate;
    }
}
