using Cinemachine;
using Photon.Pun;
using UnityEngine;

namespace SkyDocker
{
    public class View : MonoBehaviourPunCallbacks
    {
        [SerializeField] private CinemachineFreeLook _lookCam;
        [SerializeField] private CinemachineVirtualCamera _frontCam;
        
        private GameObject _frontView;
        private GameInput _input;

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;

            _input = new GameInput();
            _input.Spacecraft.ToggleView.performed += context => ToggleView();

            if (!photonView.IsMine)
            {
                _lookCam.Priority = 0;
            }

            if (photonView.IsMine)
            {
                _frontView = GameObject.Find("FrontView");
                _frontView.SetActive(false);
            }

            
        }

        public override void OnEnable()
        {
            base.OnEnable();
            _input.Spacecraft.Enable();
        }

        public override void OnDisable()
        {
            base.OnDisable();
            _input.Spacecraft.Disable();
        }

        private void ToggleView()
        {
            if (!photonView.IsMine) return;

            if (_lookCam.Priority == 1)
            {
                _lookCam.Priority--;
                _frontCam.Priority++;
                _frontView.SetActive(true);
            }
            else
            {
                _frontCam.Priority--;
                _lookCam.Priority++;
                _frontView.SetActive(false);
            }
        }
    }
}
