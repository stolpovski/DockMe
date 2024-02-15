using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

namespace DockMe
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private GameObject _playerPrefab;

        [SerializeField]
        private int _positionRange;

        [SerializeField]
        private int _rotationRange;

        private void Start()
        {
            PhotonNetwork.Instantiate(_playerPrefab.name, Randomizer.Position(_positionRange), Randomizer.Rotation(_rotationRange));
        }

        public override void OnLeftRoom()
        {
            SceneManager.LoadScene(0);
        }

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }

        public override void OnPlayerEnteredRoom(Player other)
        {
            Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName);
        }

        public override void OnPlayerLeftRoom(Player other)
        {
            Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName);
        }
    }
}