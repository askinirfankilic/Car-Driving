using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Data;
using UnityEngine;
using UnityEngine.Serialization;
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

    private float _speed = 5;
    private float _rotationSpeed = 180f;

    [SerializeField]
    private bool _isActive;
    [SerializeField]
    private bool _isCurrent = false;

    private CheckLeftRight _checkLeftRight;
    private PlayerInput _playerInput;
    private CarSettings _carSettings;

    private IEnumerable<ScreenSide> _inputDataEnumerable;
    private IEnumerator _inputDataEnumerator;

    [Inject]
    private void Construct(CheckLeftRight checkLeftRight, PlayerInput playerInput, CarSettings carSettings)
    {
        _checkLeftRight = checkLeftRight;
        _playerInput = playerInput;
        _carSettings = carSettings;

        _speed = _carSettings.Speed;
        _rotationSpeed = _carSettings.RotationSpeed;
    }

    public void InitializeAutoControlled()
    {
        _inputDataEnumerable = _movementData.ScreenInputs;
        _inputDataEnumerator = _inputDataEnumerable.GetEnumerator();
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
            Vector3 movement = transform.up * (_speed * Time.fixedDeltaTime);
            transform.position += movement;

            _movementData.ScreenInputs.Add(_checkLeftRight.Side);

            switch (_checkLeftRight.Side)
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
        else
        {
            Vector3 movement = transform.up * (_speed * Time.fixedDeltaTime);
            transform.position += movement;

            if (_inputDataEnumerator.MoveNext())
            {
                switch (_inputDataEnumerator.Current)
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
    }
}