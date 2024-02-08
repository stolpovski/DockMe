using Cinemachine;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DockMe
{
    public class Spacecraft : MonoBehaviourPunCallbacks, IPunObservable
    {
        public float Propellant = 1f;
        public CinemachineFreeLook lookCam;

        [SerializeField]
        public GameObject PlayerUiPrefab;

        private Rigidbody rb;


        [SerializeField]
        private Vector3 vel;

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                // We own this player: send the others our data
                stream.SendNext(Propellant);
            }
            else
            {
                // Network player, receive data
                this.Propellant = (float)stream.ReceiveNext();
            }
        }

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
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

        public void UpdatePropellant()
        {
            if (photonView.IsMine)
            {
                photonView.RPC("UpdateProp", RpcTarget.OthersBuffered, Propellant);
            }
        }

        [PunRPC]
        private void UpdateProp(float prop)
        {
            Propellant = prop;
        }

        private void Update()
        {
            vel = rb.velocity;
        }


    }
}

