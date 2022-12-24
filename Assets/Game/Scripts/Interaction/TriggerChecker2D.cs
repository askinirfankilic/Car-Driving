using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TriggerChecker2D : MonoBehaviour
{
    public Action<Collider2D> OnTrigerEntered;
    public Action<Collider2D> OnTrigerStayed;
    public Action<Collider2D> OnTrigerExitted;

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnTrigerEntered?.Invoke(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        OnTrigerStayed?.Invoke(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        OnTrigerExitted?.Invoke(other);
    }
}