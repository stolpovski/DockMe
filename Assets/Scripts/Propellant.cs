using Photon.Pun;
using UnityEngine;

namespace DockMe
{
    public class Propellant : MonoBehaviour, IPunObservable
    {
        public float MaxAmount = 40f;
        public float Amount;
        [SerializeField] private bool _isEndless;
        public float RelativeAmount => Amount / MaxAmount;
        public bool IsEmpty => Amount <= 0f;

        private void Awake()
        {
            Amount = MaxAmount;
        }

        public void Burn(float amount)
        {
            if (_isEndless)
            {
                return;
            }
            Amount = Mathf.Clamp(Amount - amount, 0f, MaxAmount);
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                // We own this player: send the others our data
                stream.SendNext(Amount);
            }
            else
            {
                // Network player, receive data
                Amount = (float)stream.ReceiveNext();
            }
        }
    }
}
