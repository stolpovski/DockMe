using Photon.Voice.Unity;
using UnityEngine;

namespace SkyDocker
{
    public class Radio : MonoBehaviour
    {
        [SerializeField] private Recorder _recorder;

        private AudioSource _beep;

        private void Awake()
        {
            _beep = GetComponent<AudioSource>();
        }

        public void StartTransmit()
        {
            _recorder.TransmitEnabled = true;
        }

        public void StopTransmit()
        {
            _recorder.TransmitEnabled = false;
        }

        public void Beep()
        {
            _beep.Play();
        }
    }
}
