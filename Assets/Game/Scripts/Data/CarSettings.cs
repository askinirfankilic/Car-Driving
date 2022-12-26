using UnityEngine;

namespace Game.Scripts.Data
{
    [CreateAssetMenu(fileName = "CarSettingsData", menuName = "Data/CarSettings", order = 0)]
    public class CarSettings : ScriptableObject
    {
        public float Speed;
        public float RotationSpeed;
    }
}