using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyDocker
{
    public class Probe : MonoBehaviour
    {
        private void Start()
        {
            Debug.Log("probe start");
        }
        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("collision!");
        }
    }
}
