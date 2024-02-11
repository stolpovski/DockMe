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
        private TMP_Text _propellant;

        [SerializeField]
        private TMP_Text _rate;

        private Spacecraft _spacecraft;

        private void Awake()
        {
            this.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
        }

        private void Update()
        {
            _propellant.text = _spacecraft.Propellant.Amount.ToString("F1", CultureInfo.InvariantCulture);
            _rate.text = _spacecraft.Rate.ToString("F2", CultureInfo.InvariantCulture);
        }

        public void SetSpacecraft(Spacecraft spacecraft)
        {
            _spacecraft = spacecraft;
            
        }
    }
}

