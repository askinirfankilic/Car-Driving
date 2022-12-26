using System;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public bool Current
    {
        get => _current;
        set
        {
            _current = value;
            ShowCurrentExitIndicator(_current);
        }
    }


    [SerializeField]
    private CarInteraction _car;
    [SerializeField]
    private CarMovement _carMovement;
    [SerializeField]
    private ExitInteraction _exit;
    [SerializeField]
    private bool _current = false;

    private Vector3 _carStartingPosition;
    private Vector3 _carStartingRotation;

    private void Awake()
    {
        _carStartingPosition = _car.transform.position;
        _carStartingRotation = _car.transform.eulerAngles;
    }

    public void Initialize()
    {
        _car.transform.position = _carStartingPosition;
        _car.transform.eulerAngles = _carStartingRotation;
    }

    public void SetCarActive(bool state)
    {
        _carMovement.IsActive = state;
    }

    public void SetCarCurrent(bool state)
    {
        _carMovement.IsCurrent = state;
        _exit.IsCurrent = state;
    }

    public void InitializeAutoControlledCar()
    {
        _carMovement.InitializeAutoControlled();
    }

    private void ShowCurrentExitIndicator(bool state)
    {
        _exit.ShowCurrentExitIndicator(state);
    }
}