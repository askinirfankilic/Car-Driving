using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ScreenSide
{
    None = 0,
    Left = 1,
    Right = 2
}

public class CheckLeftRight : MonoBehaviour
{
    /// <summary>
    /// Side of screen which player interacted. 
    /// </summary>
    public ScreenSide Side => _screenSide;

    [SerializeField]
    private ScreenSide _screenSide = ScreenSide.None;

    private int _screenWidth;
    private PlayerInput _playerInput;

    private void Awake()
    {
        _screenWidth = Screen.width;

        TryGetComponent(out _playerInput);
        _playerInput.OnMouseButtonDown += OnMouseButtonDownHandler;
        _playerInput.OnMouseButtonStay += OnMouseButtonStayHandler;
        _playerInput.OnMouseButtonUp += OnMouseButtonUpHandler;
    }

    private void OnDestroy()
    {
        _playerInput.OnMouseButtonDown -= OnMouseButtonDownHandler;
        _playerInput.OnMouseButtonStay -= OnMouseButtonStayHandler;
        _playerInput.OnMouseButtonUp -= OnMouseButtonUpHandler;
    }

    private void CheckSide(Vector2 position)
    {
        float positionX = position.x;

        if (positionX <= _screenWidth * 0.5f)
        {
            _screenSide = ScreenSide.Left;
        }
        else
        {
            _screenSide = ScreenSide.Right;
        }
    }

    private void OnMouseButtonDownHandler(Vector2 position)
    {
        CheckSide(position);
    }

    private void OnMouseButtonStayHandler(Vector2 position)
    {
        CheckSide(position);
    }

    private void OnMouseButtonUpHandler(Vector2 position)
    {
        _screenSide = ScreenSide.None;
    }
}