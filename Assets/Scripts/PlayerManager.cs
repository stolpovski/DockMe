using Cinemachine;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DockMe
{
    public class PlayerManager : MonoBehaviourPunCallbacks
    {
        public float Propellant = 1f;
        public CinemachineFreeLook lookCam;

        [SerializeField]
        public GameObject PlayerUiPrefab;

        private void Awake()
        {
            if (!photonView.IsMine)
            {
                lookCam.Priority = 0;
            }
        }

        private void Start()
        {
            if (PlayerUiPrefab != null)
            {
                GameObject _uiGo = Instantiate(PlayerUiPrefab);
                _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
            }
            else
            {
                Debug.LogWarning("<Color=Red><a>Missing</a></Color> PlayerUiPrefab reference on player Prefab.", this);
            }
        }


    }
}

