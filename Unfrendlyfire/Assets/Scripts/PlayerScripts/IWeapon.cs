using UnityEngine;

namespace PlayerScripts
{
    public abstract class IWeapon : MonoBehaviour
    {
        public float Left  { get; set; }
        public float Right   { get; set; }
        public float Up      { get; set; }
        public float Down       { get; set; }
        
        public float RotationOffset  { get; set; }

        public abstract void Attack();
    }
}