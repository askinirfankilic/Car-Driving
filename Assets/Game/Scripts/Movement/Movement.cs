using UnityEngine;
using Zenject;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5;
    [SerializeField]
    private float _rotationSpeed = 180f;
    [SerializeField]
    private bool _isActive;

    private CheckLeftRight _checkLeftRight;
    private PlayerInput _playerInput;

    [Inject]
    private void Construct(CheckLeftRight checkLeftRight, PlayerInput playerInput)
    {
        _checkLeftRight = checkLeftRight;
        _playerInput = playerInput;

        _playerInput.OnMouseButtonDown += OnMouseButtonDown;
    }

    private void OnMouseButtonDown(Vector2 position)
    {
        _isActive = true;
        _playerInput.OnMouseButtonDown -= OnMouseButtonDown;
    }

    private void Update()
    {
        if (!_isActive) return;

        Vector3 movement = transform.up * (_speed * Time.deltaTime);
        transform.position += movement;

        switch (_checkLeftRight.Side)
        {
            case ScreenSide.None:
            {
                return;
            }
            case ScreenSide.Left:
            {
                float turn = +1 * _rotationSpeed * Time.deltaTime;
                Quaternion turnRotation = Quaternion.Euler(0, 0, turn);
                transform.rotation *= turnRotation;
                break;
            }
            case ScreenSide.Right:
            {
                float turn = -1 * _rotationSpeed * Time.deltaTime;
                Quaternion turnRotation = Quaternion.Euler(0, 0, turn);
                transform.rotation *= turnRotation;
                break;
            }
        }
    }
}