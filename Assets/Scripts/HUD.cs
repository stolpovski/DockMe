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

        [SerializeField] private TMP_Text _log;

        private Spacecraft _spacecraft;
        private GameObject _drogue;

        private void Awake()
        {
            //Cursor.lockState = CursorLockMode.Locked;
            this.transform.SetParent(GameObject.Find("Hud").GetComponent<Transform>(), false);
            _drogue = GameObject.Find("Drogue");
        }

        private void Update()
        {
            
            if (!_spacecraft) return;

            if (_drogue != null)
            {
                //Debug.Log(_spacecraft.Probe.transform.eulerAngles);
            }

            _fuelRateRange.text = String.Format(
                CultureInfo.InvariantCulture,
                "{0:F2}\n{1:F2}\n{2:F2}",
                _spacecraft.Propellant.Amount,
                _spacecraft.Rate,
                Vector3.Distance(_spacecraft.Probe.transform.position, _drogue.transform.position)
            );

            _deltaAngle.text = String.Format(
                CultureInfo.InvariantCulture,
                "{0:F2}\n{1:F2}\n{2:F2}",
                Mathf.DeltaAngle(_spacecraft.Probe.transform.eulerAngles.x, _drogue.transform.eulerAngles.x),
                Mathf.DeltaAngle(_spacecraft.Probe.transform.eulerAngles.y, _drogue.transform.eulerAngles.y),
                Mathf.DeltaAngle(_spacecraft.Probe.transform.eulerAngles.z, _drogue.transform.eulerAngles.z)
          
            );

            _angularVelocity.text = String.Format(
                CultureInfo.InvariantCulture,
                "{0:F3}\n{1:F3}\n{2:F3}",
                _spacecraft.AngularVelocity.x * RadianDegrees,
                _spacecraft.AngularVelocity.y * RadianDegrees,
                _spacecraft.AngularVelocity.z * RadianDegrees
            );

            UpdateDistance();

            

            _velocity.text = String.Format(
                CultureInfo.InvariantCulture,
                "{0:F3}\n{1:F3}\n{2:F3}",
                _spacecraft.Velocity.x,
                _spacecraft.Velocity.y,
                _spacecraft.Velocity.z
            );

            string logs = "";
            foreach (string log in _spacecraft.Log)
            {
                logs += log + "\n";
            }

            _log.text = logs;

        }

        private void UpdateDistance()
        {
            Vector3 drogueLocal = _drogue.transform.InverseTransformPoint(_spacecraft.Probe.transform.position);
            Vector3 probeLocal = _spacecraft.Probe.transform.InverseTransformPoint(_drogue.transform.position);
            
            _range.text = String.Format(
                CultureInfo.InvariantCulture,
                "{0:F2}\n{1:F2}\n{2:F2}",
                drogueLocal.x - probeLocal.x,
                drogueLocal.y - probeLocal.y,
                drogueLocal.z - probeLocal.z

            );
        }

        public void SetSpacecraft(Spacecraft spacecraft)
        {
            _spacecraft = spacecraft;
            
        }
    }
}

