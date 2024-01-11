using System;
using UnityEngine;

public class Engine : MonoBehaviour
{
    [SerializeField]
    private float _force = 1f;

    [SerializeField]
    private ParticleSystem _vfx;

    [SerializeField]
    private AudioSource _sfx;

    [NonSerialized]
    public Rigidbody RB;

    private bool _isRunning;

    public void Run()
    {
        _vfx.Play();
        _sfx.Play();
        _isRunning = true;
    }

    public void Stop()
    {
        _vfx.Stop();
        _sfx.Stop();
        _isRunning = false;
    }

    private void FixedUpdate()
    {
        if (_isRunning)
        {
            RB.AddForceAtPosition(transform.rotation * Vector3.forward * _force, transform.position, ForceMode.Impulse);
        }
    }
}
