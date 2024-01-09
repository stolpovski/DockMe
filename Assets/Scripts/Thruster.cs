using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : MonoBehaviour
{
    public Rigidbody body;
    private bool _isBurning;
    [SerializeField]
    private ParticleSystem vfx;

    [SerializeField]
    private AudioSource sfx;

    public void Burn()
    {
        _isBurning = true;
        vfx.Play();
        sfx.Play();
    }

    public void Stop()
    {
        _isBurning = false;
        vfx.Stop();
        sfx.Stop();
    }

    private void Update()
    {
        if (_isBurning)
        {
            body.AddForceAtPosition(transform.rotation * Vector3.forward * 1, transform.position);
        }
    }
}
