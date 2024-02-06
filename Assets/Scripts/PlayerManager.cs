using Cinemachine;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DockMe
{
    public class PlayerManager : MonoBehaviourPunCallbacks
    {
        public float Propellant = 10f;
        public CinemachineFreeLook lookCam;

        private void Awake()
        {
            if (!photonView.IsMine)
            {
                lookCam.Priority = 0;
            }
        }
    }
}

