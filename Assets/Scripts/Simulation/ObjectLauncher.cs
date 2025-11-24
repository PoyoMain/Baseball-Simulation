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
    private const string SPIN_SPEED_KEY = "SpinSpeed";

    public static UnityAction<Rigidbody> OnObjectLaunched;

    private SeamCount seamCount;
    private Spin spin;
    private int spinSpeed;
    private int increment;
    private Vector3 twoSeamCountEulers = new(0f, 0f, -90f);
    private Vector3 fourSeamCountEulers = new(-90f, 0f, 0f);

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
        spinSpeed = PlayerPrefs.GetInt(SPIN_SPEED_KEY);
        increment = PlayerPrefs.GetInt(INCREMENT_KEY);
    }

    public void LaunchObject()
    {
        if (launchedObjectPrefab == null) Debug.LogError("Object to launch not set in inspector");

        Rigidbody launchedObject = Instantiate(launchedObjectPrefab, transform.position, Quaternion.identity, transform);
        Vector3 eulers = seamCount switch
        {
            SeamCount.TwoCount => twoSeamCountEulers,
            SeamCount.FourCount => fourSeamCountEulers,
            SeamCount.Mixed => Random.Range(0, 2) == 0 ? fourSeamCountEulers : twoSeamCountEulers,
            _ => throw new NotImplementedException(),
        };
        launchedObject.transform.eulerAngles = eulers;

        Vector3 forceVector = direction * MPHtoMetersPerSecond(initVelocityMPH);
        if (useForce) launchedObject.AddForce(forceVector, ForceMode.VelocityChange);
        else launchedObject.linearVelocity = forceVector;

        if (spin == Spin.Mixed)
        {
            int choice = Random.Range(0, 5); 
            spin = choice switch
            {
                1 => Spin.Backspin,
                2 => Spin.Sidespin,
                3 => Spin.Topspin,
                4 => Spin.NoSpin,
                _ => throw new NotImplementedException(),
            };
        }

        Vector3 torqueVector = spin switch
        {
            Spin.Backspin => Vector3.left,
            Spin.Sidespin => Vector3.forward,
            Spin.Topspin => Vector3.right,
            Spin.NoSpin => Vector3.zero,
            _ => throw new NotImplementedException(),
        } * spinSpeed;
        if (useTorque) launchedObject.AddTorque(torqueVector, ForceMode.VelocityChange);
        else launchedObject.angularVelocity = torqueVector;

        OnObjectLaunched?.Invoke(launchedObject);

        initVelocityMPH += increment;
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
