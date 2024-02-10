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
        private Vector3 vel;




        private void Awake()
        {
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
        }


        

    }
}

