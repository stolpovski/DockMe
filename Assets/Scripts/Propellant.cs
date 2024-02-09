using Photon.Pun;
using UnityEngine;

namespace DockMe
{
    public class Propellant : MonoBehaviour, IPunObservable
    {
        public float Amount = 1f;
        public bool IsEmpty => Amount <= 0f;

        public void Burn(float amount)
        {
            Amount -= amount;
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
