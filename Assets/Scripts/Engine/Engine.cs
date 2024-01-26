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

    private bool isBurning;

    public void StartBurning()
    {
        
        isBurning = true;
    }

    public void StopBurning()
    {
        
        isBurning = false;
    }

    public void StartVFX()
    {
        _vfx.Play();
    }

    public void StopVFX()
    {
        _vfx.Stop();
    }

    public void StartSFX()
    {
        _sfx.Play();
    }

    public void StopSFX()
    {
        _sfx.Stop();

    }

    private void FixedUpdate()
    {
        if (isBurning)
        {
            RB.AddForceAtPosition(transform.rotation * Vector3.forward * _force, transform.position, ForceMode.Impulse);
        }
    }
}
