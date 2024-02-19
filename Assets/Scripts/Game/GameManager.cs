using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

namespace DockMe
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private GameObject _stationPrefab;
        
        [SerializeField]
        private GameObject _spacecraftPrefab;

        private GameObject _station;

        [SerializeField] private int _rangeXY;
        [SerializeField] private int _minZ;
        [SerializeField] private int _maxZ;

        [SerializeField] private int _rotationRange;

        private void Start()
        {
            if (!_station)
            {
                _station = Instantiate(_stationPrefab, Vector3.zero, Quaternion.Euler(
                    Random.Range(-_rotationRange, _rotationRange),
                    Random.Range(-_rotationRange, _rotationRange),
                    Random.Range(-_rotationRange, _rotationRange)
                ));
            }

            PhotonNetwork.Instantiate(
                _spacecraftPrefab.name,
                new Vector3(Random.Range(-_rangeXY, _rangeXY), Random.Range(-_rangeXY, _rangeXY), Random.Range(_minZ, _maxZ)),
                Quaternion.Euler(
                    Random.Range(-_rotationRange, _rotationRange), 
                    Random.Range(-_rotationRange, _rotationRange), 
                    Random.Range(-_rotationRange, _rotationRange)
                )
            );
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