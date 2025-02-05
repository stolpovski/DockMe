using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyDocker
{
    public class Drogue : MonoBehaviour
    {
        public Transform probe;
        private bool _hasDocked;
        private AudioSource _confirmed;
        /*private void OnTriggerEnter(Collider other)
        {
            if (_hasDocked)
            {
                return;
            }
            Debug.Log(other.attachedRigidbody.velocity.magnitude);

            other.attachedRigidbody.isKinematic = true;
            _hasDocked = true;
        }*/

        private void Awake()
        {
            _confirmed = GetComponent<AudioSource>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_hasDocked) return;
            /*Debug.Log(collision.rigidbody.velocity.magnitude);
            
            Debug.Log(collision.contacts.Length);*/
            Debug.Log(collision.collider);
            Debug.Log(collision.relativeVelocity.magnitude);
            collision.rigidbody.isKinematic = true;
            _confirmed.Play();
            _hasDocked = true;

            Spacecraft spacecraft = collision.gameObject.GetComponent<Spacecraft>();
            spacecraft.CompleteDocking();


        }

        private void Update()
        {
            if (probe)
            Debug.Log(Vector3.Distance(transform.position, probe.position));
        }
    }
}

