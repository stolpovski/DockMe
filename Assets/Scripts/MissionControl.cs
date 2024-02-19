using Photon.Voice.Unity;
using UnityEngine;

namespace DockMe
{
    public class MissionControl : MonoBehaviour
    {
        [SerializeField]
        private Recorder _recorder;

        public void StartTransmit()
        {
            _recorder.TransmitEnabled = true;
        }

        public void StopTransmit()
        {
            _recorder.TransmitEnabled = false;
        }
    }
}
