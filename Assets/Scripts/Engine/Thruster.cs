using System;
using UnityEngine;

namespace DockMe
{
    public class Thruster : MonoBehaviour
    {
        [NonSerialized] public Rigidbody Body;
        [NonSerialized] public Spacecraft Craft;

        [SerializeField] private float _force = 1f;
        [SerializeField] private ParticleSystem _vfx;
        [SerializeField] private AudioSource _sfx;
        [SerializeField] private bool _isRunning;

        public void Ignite()
        {
            if (Craft.Propellant <= 0)
            {
                return;
            }
            _isRunning = true;
        }

        public void IgniteVfx()
        {
            
            _vfx.Play();
        }

        public void IgniteSfx()
        {
            
            _sfx.Play();
        }

        public void Cutoff()
        {
            _isRunning = false;
            //Craft.UpdatePropellant();
        }

        public void CutoffVfx()
        {
            _vfx.Stop();
        }

        public void CutoffSfx()
        {
            _sfx.Stop();
        }

        private void FixedUpdate()
        {
            if (_isRunning)
            {
                if (Craft.Propellant < 0)
                {
                    Cutoff();
                    CutoffVfx();
                    CutoffSfx();
                }
                
                Body.AddForceAtPosition(transform.rotation * Vector3.forward * _force, transform.position, ForceMode.Impulse);
                Craft.Propellant -= 0.01f;
                
            }

            
        }
    }
}

