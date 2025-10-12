using System;
using TMPro;
using UnityEngine;

public class PersonalInformationUI : MonoBehaviour
{
    [Header("Menu")]
    [SerializeField] private GameObject menu;

    [Header("Dropdowns")]
    [SerializeField] private TMP_Dropdown currentBaseballLevelDropdown;
    [SerializeField] private TMP_Dropdown battingVisionDropdown;
    [SerializeField] private TMP_Dropdown preferedPitchDropdown;
    [SerializeField] private TMP_Dropdown nonPreferedPitchDropdown;
    [SerializeField] private TMP_Dropdown hitterTypeDropdown;

    [Header("Input Fields")]
    [SerializeField] private TMP_InputField currentBaseballLevelInputField;
    [SerializeField] private TMP_InputField battingVisionInputField;
    [SerializeField] private TMP_InputField preferedPitchInputField;
    [SerializeField] private TMP_InputField nonPreferedPitchInputField;
    [SerializeField] private TMP_InputField hitterTypeInputField;

    private int currentBaseballLevelOtherIndex;
    private int battingVisionOtherIndex;
    private int preferedPitchOtherIndex;
    private int nonPreferedPitchOtherIndex;
    private int hitterTypeOtherIndex;

    private void Start()
    {
        currentBaseballLevelOtherIndex = currentBaseballLevelDropdown.options.Count - 1;
        battingVisionOtherIndex = battingVisionDropdown.options.Count - 1;
        preferedPitchOtherIndex = preferedPitchDropdown.options.Count - 1;
        nonPreferedPitchOtherIndex = nonPreferedPitchDropdown.options.Count - 1;
        hitterTypeOtherIndex = hitterTypeDropdown.options.Count - 1;
    }

    public void ToggleCurrentBaseballLevelInputField(Int32 value)
    {
        if (value == currentBaseballLevelOtherIndex) currentBaseballLevelInputField.gameObject.SetActive(true);
        else currentBaseballLevelInputField.gameObject.SetActive(false);
    }

    public void ToggleBattingVisionInputField(Int32 value)
    {
        if (value == battingVisionOtherIndex) battingVisionInputField.gameObject.SetActive(true);
        else battingVisionInputField.gameObject.SetActive(false);
    }

    public void TogglePreferedPitchInputField(Int32 value)
    {
        if (value == preferedPitchOtherIndex) preferedPitchInputField.gameObject.SetActive(true);
        else preferedPitchInputField.gameObject.SetActive(false);
    }

    public void ToggleNonPreferedPitchInputField(Int32 value)
    {
        if (value == nonPreferedPitchOtherIndex) nonPreferedPitchInputField.gameObject.SetActive(true);
        else nonPreferedPitchInputField.gameObject.SetActive(false);
    }

    public void ToggleHitterTypeInputField(Int32 value)
    {
        if (value == hitterTypeOtherIndex) hitterTypeInputField.gameObject.SetActive(true);
        else hitterTypeInputField.gameObject.SetActive(false);
    }

    public void CloseMenu()
    {
        menu.SetActive(false);
    }
}
