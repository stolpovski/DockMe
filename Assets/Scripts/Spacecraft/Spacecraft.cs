using Cinemachine;
using Photon.Pun;
using UnityEngine;

namespace DockMe
{
    public class Spacecraft : MonoBehaviourPunCallbacks
    {
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

