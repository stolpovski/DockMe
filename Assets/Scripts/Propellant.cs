using UnityEngine;

namespace DockMe
{
    public class Propellant : MonoBehaviour
    {
        public float Amount = 1f;
        public bool IsEmpty => Amount <= 0f;

        public void Burn(float amount)
        {
            Amount -= amount;
        }
    }
}
