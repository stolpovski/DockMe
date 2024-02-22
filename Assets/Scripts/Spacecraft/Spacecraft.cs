using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SkyDocker
{
    public class Spacecraft : MonoBehaviourPunCallbacks
    {
        [NonSerialized] public Propellant Propellant;
        
        public GameObject PlayerUiPrefab;

        [SerializeField]
        private GameObject _hudPrefab;
        
        [SerializeField]
        private Vector3 vel;

        private Rigidbody _rigidbody;

        public float Rate => _rigidbody.velocity.magnitude;
        public float Range => Vector3.Distance(transform.position, Vector3.zero);

        public Vector3 DeltaAngle => transform.localEulerAngles;

        public Vector3 AngularVelocity => _rigidbody.angularVelocity;

        public Vector3 Position => transform.position;
        public Vector3 Velocity => transform.InverseTransformDirection(_rigidbody.velocity);

        public GameObject Probe;
        public Queue<string> Log = new Queue<string>();

        internal void CompleteDocking()
        {
            Debug.Log("CompleteDocking");
        }

        private void Awake()
        {
            
            
            _rigidbody = GetComponent<Rigidbody>();
            //_rigidbody.centerOfMass = new Vector3(0, 0, -0.7078f);
            Propellant = GetComponent<Propellant>();
            

            Log.Enqueue(photonView.Owner.NickName + " joined");


        }

        

        

        private void Start()
        {
            if (!photonView.IsMine && PlayerUiPrefab != null)
            {
                
                GameObject _uiGo = Instantiate(PlayerUiPrefab);
                _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
            }

            if (photonView.IsMine && _hudPrefab != null)
            {
                GameObject _hudGo = Instantiate(_hudPrefab);
                _hudGo.SendMessage("SetSpacecraft", this, SendMessageOptions.RequireReceiver);

            }
        }


        public override void OnPlayerEnteredRoom(Player other)
        {
            //Debug.Lo("OnPlayerEnteredRoom() {0}", other.NickName);
            if (Log.Count > 2)
            {
                Log.Dequeue();
            }

            Log.Enqueue(other.NickName + " joined");
        }

        public override void OnPlayerLeftRoom(Player other)
        {
            if (Log.Count > 2)
            {
                Log.Dequeue();
            }

            Log.Enqueue(other.NickName + " left");
        }
    }
}

