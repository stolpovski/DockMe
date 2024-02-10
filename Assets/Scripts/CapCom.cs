using Photon.Pun;
using Photon.Voice.Unity;
using UnityEngine;

namespace DockMe
{
    public class CapCom : MonoBehaviourPunCallbacks
    {
        [SerializeField] private AudioSource _introTone;
        [SerializeField] private AudioSource _outroTone;
        
        private Recorder _recorder;
        private GameInput _gameInput;

        private void Awake()
        {
            _recorder = GameObject.Find("MissionControl").GetComponent<Recorder>();

            _gameInput = new GameInput();
            _gameInput.Transponder.Transmit.performed += context => OnStartTransmit();
            _gameInput.Transponder.Transmit.canceled += context => OnStopTransmit();
        }

        public override void OnEnable()
        {
            base.OnEnable();
            _gameInput.Transponder.Enable();
        }

        public override void OnDisable()
        {
            base.OnDisable();
            _gameInput.Transponder.Disable();
        }

        private void OnStartTransmit()
        {
            //if (_missionControl.IsTransmitting) return;
            StartTransmit();
            
            if (photonView.IsMine)
            {
                photonView.RPC("StartTransmit", RpcTarget.Others);
            }
            
            _recorder.TransmitEnabled = true;
            //_missionControl.StartTransmit();
        }

        private void OnStopTransmit()
        {
            StopTransmit();
            
            if (photonView.IsMine)
            {
                photonView.RPC("PlayOutroTone", RpcTarget.Others);
            }    
                
            _recorder.TransmitEnabled = false;
        }

        [PunRPC]
        private void StartTransmit()
        {
            _outroTone.Stop();
            _introTone.Play();
            //_missionControl.StartTransmit();
        }

        [PunRPC]
        private void StopTransmit()
        {
            _introTone.Stop();
            _outroTone.Play();
        }
    }
}
