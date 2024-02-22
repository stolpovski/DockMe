using Photon.Pun;
using UnityEngine;

namespace SkyDocker
{
    public class Flashlight : MonoBehaviourPunCallbacks
    {
        private GameInput _input;
        [SerializeField] private Light _flashlight;

        private void Awake()
        {
            _input = new GameInput();
            _input.Spacecraft.ToggleFlashlight.performed += context => OnToggleFlashlight();
        }

        override public void OnEnable()
        {
            base.OnEnable();
            _input.Spacecraft.Enable();
        }

        override public void OnDisable()
        {
            base.OnDisable();
            _input.Spacecraft.Disable();
        }

        private void OnToggleFlashlight()
        {
            if (!photonView.IsMine)
            {
                return;
            }

            photonView.RPC("ToggleFlashlight", RpcTarget.All);
        }

        [PunRPC]
        private void ToggleFlashlight()
        {
            if (!_flashlight.enabled)
            {
                _flashlight.enabled = true;
            }
            else
            {
                _flashlight.enabled = false;
            }
        }
    }
}
