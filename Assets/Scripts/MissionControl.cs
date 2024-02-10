using Photon.Voice.Unity;
using UnityEngine;

namespace DockMe
{
    public class MissionControl : MonoBehaviour
    {
        [SerializeField] private AudioSource _introBeep;
        [SerializeField] private AudioSource _outroBeep;

        [SerializeField]
        private Recorder _recorder;

        public bool IsTransmitting => _recorder.TransmitEnabled;

        public void StartTransmit()
        {
            _introBeep.Play();
            _recorder.TransmitEnabled = true;
        }

        public void StopTransmit()
        {
            _outroBeep.Play();
            _recorder.TransmitEnabled = false;
        }
    }
}

