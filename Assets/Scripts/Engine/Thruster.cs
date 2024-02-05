using System;
using UnityEngine;

public class Thruster : MonoBehaviour
{
    [NonSerialized] public Rigidbody Body;
    
    [SerializeField] private float _force = 1f;
    [SerializeField] private ParticleSystem _vfx;
    [SerializeField] private AudioSource _sfx;
    [SerializeField] private bool _isRunning;

    public void Ignite()
    {
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
            Body.AddForceAtPosition(transform.rotation * Vector3.forward * _force, transform.position, ForceMode.Impulse);
        }
    }
}
