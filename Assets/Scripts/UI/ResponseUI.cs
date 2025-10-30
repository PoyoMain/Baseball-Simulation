using System;
using UnityEngine;
using UnityEngine.UI;

public class ResponseUI : MonoBehaviour
{
    [SerializeField] private GameObject menu;

    [Header("Fields")]
    [SerializeField] private Toggle[] toggles;

    private void OnEnable()
    {
        Wall.OnObjectHitWall.AddListener(EnableMenu);
    }

    private void OnDisable()
    {
        Wall.OnObjectHitWall.RemoveListener(EnableMenu);
    }

    private void EnableMenu()
    {
        menu.SetActive(true);
    }

    public void Submit()
    {
        menu.SetActive(false);

        foreach (Toggle toggle in toggles)
        {
            toggle.isOn = false;
        }
    }
}
