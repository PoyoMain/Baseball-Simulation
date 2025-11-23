using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private GameObject menu;

    [Header("Fields")]
    [SerializeField] private TMP_InputField distanceInputField;
    [SerializeField] private TMP_InputField minimalSpeedInputField;
    [SerializeField] private TMP_InputField maximalSpeedInputField;
    [SerializeField] private TMP_InputField incrementInputField;
    [SerializeField] private Toggle randomizeSpeedToggle;
    [SerializeField] private Toggle displaySpeedToggle;
    [SerializeField] private TMP_Dropdown seamCountDropDown;
    [SerializeField] private TMP_Dropdown spinDropDown;
    [SerializeField] private Toggle skipPersonalInfoToggle;
    [SerializeField] private TMP_Dropdown simulationTriggerDropDown;

    private const string DISTANCE_KEY = "Distance";
    private const string MINIMAL_SPEED_KEY = "MinimalSpeed";
    private const string MAXIMAL_SPEED_KEY = "MaximalSpeed";
    private const string INCREMENT_KEY = "Increment";
    private const string RANDOMIZE_SPEED_KEY = "RandomizeSpeed";
    private const string DISPLAY_SPEED_KEY = "DisplaySpeed";
    private const string SEAM_COUNT_KEY = "SeamCount";
    private const string SPIN_KEY = "Spin";
    private const string SKIP_PERSONAL_INFO_KEY = "SkipPersonalInfo";
    private const string SIMULATION_TRIGGER_KEY = "SimulationTrigger";

    public static UnityAction OnValuesUpdated;

    private void Start()
    {
        SetValuesFromPrefs();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) ShowMenu();
    }

    private void SetValuesFromPrefs()
    {
        if (PlayerPrefs.HasKey(DISTANCE_KEY)) distanceInputField.text = PlayerPrefs.GetFloat(DISTANCE_KEY).ToString();
        else distanceInputField.text = "60.5";
        if (PlayerPrefs.HasKey(MINIMAL_SPEED_KEY)) minimalSpeedInputField.text = PlayerPrefs.GetInt(MINIMAL_SPEED_KEY).ToString();
        else minimalSpeedInputField.text = "80";
        if (PlayerPrefs.HasKey(MAXIMAL_SPEED_KEY)) maximalSpeedInputField.text = PlayerPrefs.GetInt(MAXIMAL_SPEED_KEY).ToString();
        else maximalSpeedInputField.text = "100";
        if (PlayerPrefs.HasKey(INCREMENT_KEY)) incrementInputField.text = PlayerPrefs.GetInt(INCREMENT_KEY).ToString();
        else incrementInputField.text = "5";

        if (PlayerPrefs.HasKey(RANDOMIZE_SPEED_KEY)) randomizeSpeedToggle.isOn = PlayerPrefs.GetInt(RANDOMIZE_SPEED_KEY) != 0;
        else randomizeSpeedToggle.isOn = false;
        if (PlayerPrefs.HasKey(DISPLAY_SPEED_KEY)) displaySpeedToggle.isOn = PlayerPrefs.GetInt(DISPLAY_SPEED_KEY) != 0;
        else displaySpeedToggle.isOn = false;

        if (PlayerPrefs.HasKey(SEAM_COUNT_KEY)) seamCountDropDown.value = PlayerPrefs.GetInt(SEAM_COUNT_KEY);
        else seamCountDropDown.value = 0;
        if (PlayerPrefs.HasKey(SPIN_KEY)) spinDropDown.value = PlayerPrefs.GetInt(SPIN_KEY);
        else spinDropDown.value = 0;

        if (PlayerPrefs.HasKey(SKIP_PERSONAL_INFO_KEY)) skipPersonalInfoToggle.isOn = PlayerPrefs.GetInt(SKIP_PERSONAL_INFO_KEY) != 0;
        else skipPersonalInfoToggle.isOn = false;
        if (PlayerPrefs.HasKey(SIMULATION_TRIGGER_KEY)) simulationTriggerDropDown.value = PlayerPrefs.GetInt(SIMULATION_TRIGGER_KEY);
        else simulationTriggerDropDown.value = 0;
    }

    public void UpdateValues()
    {
        PlayerPrefs.SetFloat(DISTANCE_KEY, float.Parse(distanceInputField.text));
        PlayerPrefs.SetInt(MINIMAL_SPEED_KEY, int.Parse(minimalSpeedInputField.text));
        PlayerPrefs.SetInt(MAXIMAL_SPEED_KEY, int.Parse(maximalSpeedInputField.text));
        PlayerPrefs.SetInt(INCREMENT_KEY, int.Parse(incrementInputField.text));

        PlayerPrefs.SetInt(RANDOMIZE_SPEED_KEY, randomizeSpeedToggle.isOn ? 1 : 0);
        PlayerPrefs.SetInt(DISPLAY_SPEED_KEY, displaySpeedToggle.isOn ? 1 : 0);

        PlayerPrefs.SetInt(SEAM_COUNT_KEY, seamCountDropDown.value);
        PlayerPrefs.SetInt(SPIN_KEY, spinDropDown.value);

        PlayerPrefs.SetInt(SKIP_PERSONAL_INFO_KEY, skipPersonalInfoToggle.isOn ? 1 : 0);
        PlayerPrefs.SetInt(SIMULATION_TRIGGER_KEY, simulationTriggerDropDown.value);

        OnValuesUpdated?.Invoke();
    }

    private void ShowMenu()
    {
        if (menu.activeSelf) return;
        menu.SetActive(true);
    }

    public void HideMenu()
    {
        if (!menu.activeSelf) return;
        menu.SetActive(false);
    }
}

public enum SeamCount
{
    TwoCount,
    FourCount,
    Mixed,
}

public enum Spin
{
    Backspin,
    Sidespin,
    Topspin,
    NoSpin,
    Mixed,
}

public enum SimulationTrigger
{
    Manual,
    Auto
}
