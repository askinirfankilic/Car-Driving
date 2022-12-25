using System;
using UnityEngine;

public class ExitInteraction : MonoBehaviour
{
    [SerializeField]
    private GameObject _currentExitIndicator;

    private void Awake()
    {
        ShowCurrentExitIndicator(false);
    }

    public void ShowCurrentExitIndicator(bool state)
    {
        _currentExitIndicator.SetActive(state);
    }
}
