using Cinemachine;
using Photon.Pun;
using System;
using UnityEngine;

namespace DockMe
{
    public class Spacecraft : MonoBehaviourPunCallbacks
    {
        [NonSerialized]
        public Propellant Propellant;


        public CinemachineFreeLook lookCam;

        [SerializeField]
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



        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            Propellant = GetComponent<Propellant>();
            if (!photonView.IsMine)
            {
                lookCam.Priority = 0;
            }

           
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


        

    }
}

