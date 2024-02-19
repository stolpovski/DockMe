using Photon.Pun;
using UnityEngine;

namespace DockMe
{
    public class Transponder : MonoBehaviourPunCallbacks
    {
        private GameInput _input;
        private MissionControl _missionControl;

        private void Awake()
        {
            _missionControl = GameObject.Find("MissionControl").GetComponent<MissionControl>();

            _input = new GameInput();
            _input.Transponder.Transmit.performed += context => StartTransmit();
            _input.Transponder.Transmit.canceled += context => StopTransmit();
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

        private void StartTransmit()
        {
            if (!photonView.IsMine)
            {
                return;
            }

            _missionControl.StartTransmit();
        }

        private void StopTransmit()
        {
            if (!photonView.IsMine)
            {
                return;
            }

            _missionControl.StopTransmit();
        }
    }
}

