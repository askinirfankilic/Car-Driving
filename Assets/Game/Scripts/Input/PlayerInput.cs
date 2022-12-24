using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private Vector2 _mousePosition;

    public event Action<Vector2> OnMouseButtonDown;
    public event Action<Vector2> OnMouseButtonStay;
    public event Action<Vector2> OnMouseButtonUp;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _mousePosition = Input.mousePosition;
            OnMouseButtonDown?.Invoke(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            _mousePosition = Input.mousePosition;
            OnMouseButtonStay?.Invoke(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _mousePosition = Input.mousePosition;
            OnMouseButtonUp?.Invoke(Input.mousePosition);
        }
        else
        {
            _mousePosition = Vector2.zero;
        }
    }
}