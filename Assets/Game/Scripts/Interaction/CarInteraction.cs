using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class CarInteraction : MonoBehaviour
{
    [SerializeField]
    private TriggerChecker2D _triggerChecker;

    private void Awake()
    {
        _triggerChecker.OnTrigerEntered += OnTriggerEnterHandler;
    }

    private void OnDestroy()
    {
        _triggerChecker.OnTrigerEntered -= OnTriggerEnterHandler;
    }

    private void OnTriggerEnterHandler(Collider2D other)
    {
        if (other.CompareTag(Tags.Obstacle))
        {
            Debug.Log("Obstacle!", other);
        }
    }
}