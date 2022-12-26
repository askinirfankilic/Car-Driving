using System;
using UnityEngine;

public class ExitInteraction : MonoBehaviour
{
    public bool IsCurrent = false;

    [SerializeField]
    private GameObject _visualObject;

    private void Awake()
    {
        ShowCurrent(false);
    }

    public void ShowCurrent(bool state)
    {
        _visualObject.SetActive(state);
    }
}