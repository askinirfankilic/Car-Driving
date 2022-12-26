using System;
using UnityEngine;

public class ExitInteraction : MonoBehaviour
{
    public bool IsCurrent = false;
    
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