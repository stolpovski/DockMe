using Photon.Pun;
using UnityEngine;

namespace DockMe
{
    public class Transponder : MonoBehaviourPunCallbacks
    {
        private GameInput _input;
        private MissionControl _missionControl;
        private bool _wasStarted;

        private void Awake()
        {
            _missionControl = GameObject.Find("MissionControl").GetComponent<MissionControl>();

            _input = new GameInput();
            _input.Transponder.Transmit.performed += context => OnStartTransmit();
            _input.Transponder.Transmit.canceled += context => OnStopTransmit();
        }

        public override void OnEnable()
        {
            base.OnEnable();
            _input.Transponder.Enable();
        }

        public override void OnDisable()
        {
            base.OnDisable();
            _input.Transponder.Disable();
        }

        private void OnStartTransmit()
        {
            if (!photonView.IsMine || _missionControl.IsTransmitting)
            {
                return;
            }

            photonView.RPC("StartTransmit", RpcTarget.All);
            _wasStarted = true;
        }

        private void OnStopTransmit()
        {
            if (!photonView.IsMine || !_wasStarted)
            {
                return;
            }

            photonView.RPC("StopTransmit", RpcTarget.All);
            _wasStarted = false;
        }

        [PunRPC]
        private void StartTransmit()
        {
            _missionControl.StartTransmit();
        }

        [PunRPC]
        private void StopTransmit()
        {
            _missionControl.StopTransmit();
        }
    }
}

