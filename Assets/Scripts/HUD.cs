using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace DockMe
{
    public class HUD : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _fuelRateRange;

        [SerializeField]
        private TMP_Text _deltaAngle;

        [SerializeField]
        private TMP_Text _angularVelocity;

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
                _spacecraft.AngularVelocity.x * 57.2958,
                _spacecraft.AngularVelocity.y * 57.2958,
                _spacecraft.AngularVelocity.z * 57.2958
            );





        }

        public void SetSpacecraft(Spacecraft spacecraft)
        {
            _spacecraft = spacecraft;
            
        }
    }
}

