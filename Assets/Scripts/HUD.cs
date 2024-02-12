using System;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace DockMe
{
    public class HUD : MonoBehaviour
    {
        const float RadianDegrees = 57.2958f;

        [SerializeField]
        private TMP_Text _fuelRateRange;

        [SerializeField]
        private TMP_Text _deltaAngle;

        [SerializeField]
        private TMP_Text _angularVelocity;

        [SerializeField]
        private TMP_Text _range;

        [SerializeField]
        private TMP_Text _velocity;

        private Spacecraft _spacecraft;

        private void Awake()
        {
            this.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
        }

        private void Update()
        {
            _fuelRateRange.text = String.Format(
                CultureInfo.InvariantCulture,
                "{0:F1}\n{1:F1}\n{2:F1}",
                _spacecraft.Propellant.Amount,
                _spacecraft.Rate,
                _spacecraft.Range
            );

            _deltaAngle.text = String.Format(
                CultureInfo.InvariantCulture,
                "{0:F1}\n{1:F1}\n{2:F1}",
                _spacecraft.DeltaAngle.x,
                _spacecraft.DeltaAngle.y,
                _spacecraft.DeltaAngle.z
            );

            _angularVelocity.text = String.Format(
                CultureInfo.InvariantCulture,
                "{0:F1}\n{1:F1}\n{2:F1}",
                _spacecraft.AngularVelocity.x * RadianDegrees,
                _spacecraft.AngularVelocity.y * RadianDegrees,
                _spacecraft.AngularVelocity.z * RadianDegrees
            );

            _range.text = String.Format(
                CultureInfo.InvariantCulture,
                "{0:F1}\n{1:F1}\n{2:F1}",
                _spacecraft.Position.x,
                _spacecraft.Position.y,
                _spacecraft.Position.z
            );

            _velocity.text = String.Format(
                CultureInfo.InvariantCulture,
                "{0:F1}\n{1:F1}\n{2:F1}",
                _spacecraft.Velocity.x,
                _spacecraft.Velocity.y,
                _spacecraft.Velocity.z
            );

        }

        public void SetSpacecraft(Spacecraft spacecraft)
        {
            _spacecraft = spacecraft;
            
        }
    }
}

