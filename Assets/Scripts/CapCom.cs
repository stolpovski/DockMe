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
            _gameInput.CapCom.Transmit.performed += context => EnableTransmit();
            _gameInput.CapCom.Transmit.canceled += context => DisableTransmit();
        }

        public override void OnEnable()
        {
            base.OnEnable();
            _gameInput.CapCom.Enable();
        }

        public override void OnDisable()
        {
            base.OnDisable();
            _gameInput.CapCom.Disable();
        }

        private void EnableTransmit()
        {
            PlayIntroTone();
            
            if (photonView.IsMine)
            {
                photonView.RPC("PlayIntroTone", RpcTarget.Others);
            }
            
            _recorder.TransmitEnabled = true;
        }

        private void DisableTransmit()
        {
            PlayOutroTone();
            
            if (photonView.IsMine)
            {
                photonView.RPC("PlayOutroTone", RpcTarget.Others);
            }    
                
            _recorder.TransmitEnabled = false;
        }

        [PunRPC]
        private void PlayIntroTone()
        {
            _outroTone.Stop();
            _introTone.Play();
        }

        [PunRPC]
        private void PlayOutroTone()
        {
            _introTone.Stop();
            _outroTone.Play();
        }
    }
}
