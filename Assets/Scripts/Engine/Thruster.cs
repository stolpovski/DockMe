using System;
using UnityEngine;

namespace DockMe
{
    public class Thruster : MonoBehaviour
    {
        public int Id;
        [NonSerialized] public Rigidbody Body;
        [NonSerialized] public Propellant Propellant;
        [NonSerialized] public Engine Engine;

        [SerializeField] private float _force = 1f;
        [SerializeField] private ParticleSystem _vfx;
        [SerializeField] private AudioSource _sfx;
        [SerializeField] private bool _isRunning;
        [SerializeField] private float _consumption = 0.01f;

        public void Ignite()
        {
            _isRunning = true;
            IgniteVfx();
            IgniteSfx();
            Engine.RpcIgniteVfx(Id);
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
            CutoffVfx();
            CutoffSfx();
            Engine.RpcCutoffVfx(Id);
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
                if (Propellant.IsEmpty)
                {
                    Cutoff();
                }
                
                Body.AddForceAtPosition(transform.rotation * Vector3.back * _force, transform.position, ForceMode.Impulse);
                Propellant.Burn(_consumption);
                
            }

            
        }
    }
}

