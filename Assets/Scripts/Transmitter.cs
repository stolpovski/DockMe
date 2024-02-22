using Photon.Pun;
using UnityEngine;

namespace SkyDocker
{
    public class Transmitter : MonoBehaviourPunCallbacks
    {
        private GameInput _input;
        private Radio _radio;

        private void Awake()
        {
            _input = new GameInput();
            _input.Transmitter.Transmit.performed += context => StartTransmit();
            _input.Transmitter.Transmit.canceled += context => StopTransmit();

            _radio = GameObject.Find("Radio").GetComponent<Radio>();
        }

        public override void OnEnable()
        {
            base.OnEnable();
            _input.Transmitter.Enable();
        }

        public override void OnDisable()
        {
            base.OnDisable();
            _input.Transmitter.Disable();
        }

        private void StartTransmit()
        {
            if (!photonView.IsMine)
            {
                return;
            }

            _radio.StartTransmit();
        }

        private void StopTransmit()
        {
            if (!photonView.IsMine)
            {
                return;
            }

            _radio.StopTransmit();
            photonView.RPC("Beep", RpcTarget.All);
        }

        [PunRPC]
        private void Beep()
        {
            _radio.Beep();
        }
    }
}
