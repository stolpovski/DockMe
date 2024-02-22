using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyDocker
{
    public class Docker : MonoBehaviour
    {
        [SerializeField]
        private float _maxVel = 0.04f;
        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log(collision.relativeVelocity.magnitude);

            foreach (ContactPoint contact in collision.contacts)
            {
                print(contact.thisCollider.name + " hit " + contact.otherCollider.name);
                // Visualize the contact point
                Debug.DrawRay(contact.point, contact.normal, Color.red);
            }

            if (collision.relativeVelocity.magnitude < _maxVel)
            {
                collision.rigidbody.isKinematic = true;
            }
            

            //collision.gameObject.transform.SetParent(transform);
        }
    }
}

