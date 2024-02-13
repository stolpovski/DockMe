using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DockMe
{
    public class Docker : MonoBehaviour
    {
        [SerializeField]
        private float _maxVel = 0.04f;
        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log(collision.relativeVelocity.magnitude);

            if (collision.relativeVelocity.magnitude < _maxVel)
            {
                collision.rigidbody.isKinematic = true;
            }
            

            //collision.gameObject.transform.SetParent(transform);
        }
    }
}

