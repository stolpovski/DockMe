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
/*            if (_outroBeep.isPlaying)
            {
                _outroBeep.Stop();
            }

            _introBeep.Play();*/
            _recorder.TransmitEnabled = true;
        }

        public void StopTransmit()
        {
            /*if (_introBeep.isPlaying)
            {
                _introBeep.Stop();
            }*/

            _introBeep.Play();
            _recorder.TransmitEnabled = false;
        }
    }
}

