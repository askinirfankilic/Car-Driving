using System;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(Collider2D))]
    public class TriggerChecker2D : MonoBehaviour
    {
        public Action<Collider2D> TriggerEntered;
        public Action<Collider2D> TriggerStayed;
        public Action<Collider2D> TriggerExitted;

        private void OnTriggerEnter2D(Collider2D other)
        {
            TriggerEntered?.Invoke(other);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            TriggerStayed?.Invoke(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            TriggerExitted?.Invoke(other);
        }
    }
}