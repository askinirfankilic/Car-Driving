using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Data;
using UnityEngine;
using Zenject;

public class CarMovement : MonoBehaviour
{
    /// <summary>
    /// Stores screen inputs as a screen-side format.
    /// This can be later used for auto controlled cars.
    /// </summary>
    [Serializable]
    public class MovementData
    {
        public List<ScreenSide> ScreenInputs;
    }

    public bool IsActive
    {
        get => _isActive;
        set => _isActive = value;
    }

    public bool IsCurrent
    {
        get => _isCurrent;
        set
        {
            _isCurrent = value;
            ActiveStateChanged?.Invoke(_isCurrent);
        }
    }

    public event Action<bool> ActiveStateChanged;

    [SerializeField]
    private MovementData _movementData;


    [SerializeField]
    private bool _isActive;
    [SerializeField]
    private bool _isCurrent = false;

    private float _speed;
    private float _rotationSpeed;

    private CheckLeftRight _checkLeftRight;
    private CarSettings _carSettings;

    private IEnumerable<ScreenSide> _movementDataEnumerable;
    private IEnumerator _movementDataEnumerator;

    [Inject]
    private void Construct(CheckLeftRight checkLeftRight, PlayerInput playerInput, CarSettings carSettings)
    {
        _checkLeftRight = checkLeftRight;
        _carSettings = carSettings;

        _speed = _carSettings.Speed;
        _rotationSpeed = _carSettings.RotationSpeed;
    }

    public void InitializeAutoControlled()
    {
        _movementDataEnumerable = _movementData.ScreenInputs;
        _movementDataEnumerator = _movementDataEnumerable.GetEnumerator();
    }

    public void ClearInputData()
    {
        _movementData.ScreenInputs.Clear();
    }

    private void FixedUpdate()
    {
        if (!IsActive) return;

        if (_isCurrent)
        {
            _movementData.ScreenInputs.Add(_checkLeftRight.Side);
            MoveAndRotate(_checkLeftRight.Side);
        }
        else
        {
            if (_movementDataEnumerator.MoveNext())
            {
                MoveAndRotate((ScreenSide) _movementDataEnumerator.Current);
            }
        }
    }

    private void MoveAndRotate(ScreenSide screenSide)
    {
        Move();
        Rotate(screenSide);
    }

    private void Move()
    {
        Vector3 movement = transform.up * (_speed * Time.fixedDeltaTime);
        transform.position += movement;
    }

    private void Rotate(ScreenSide screenSide)
    {
        switch (screenSide)
        {
            case ScreenSide.None:
            {
                return;
            }
            case ScreenSide.Left:
            {
                float turn = +1 * _rotationSpeed * Time.fixedDeltaTime;
                Quaternion turnRotation = Quaternion.Euler(0, 0, turn);
                transform.rotation *= turnRotation;
                break;
            }
            case ScreenSide.Right:
            {
                float turn = -1 * _rotationSpeed * Time.fixedDeltaTime;
                Quaternion turnRotation = Quaternion.Euler(0, 0, turn);
                transform.rotation *= turnRotation;
                break;
            }
        }
    }
}