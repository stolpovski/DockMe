using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    [Tooltip("The prefab to use for representing the player")]
    public GameObject playerPrefab;

    [SerializeField] private bool _randomize;
    [SerializeField] private float _randomAngle = 45f;

    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private Quaternion _startRotation;

    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
            Debug.LogFormat("PhotonNetwork : PlayerCount : {0}", PhotonNetwork.CurrentRoom.PlayerCount);

        }
    }

    public override void OnPlayerLeftRoom(Player other)
    {
        Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
            Debug.LogFormat("PhotonNetwork : PlayerCount : {0}", PhotonNetwork.CurrentRoom.PlayerCount);

        }
    }

    private void Start()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
        }
        else
        {
            if (_randomize)
            {
                _startPosition = new Vector3(Random.Range(-20, 20), Random.Range(-20, 20), Random.Range(-20, -40));
                _startRotation = Quaternion.Euler(Random.Range(-_randomAngle, _randomAngle), Random.Range(-_randomAngle, _randomAngle), Random.Range(-_randomAngle, _randomAngle));
            }
                
            // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
            PhotonNetwork.Instantiate(this.playerPrefab.name, _startPosition, _startRotation, 0);
            //PhotonNetwork.Instantiate(this.playerPrefab.name, _startPosition, _startRotation, 0);
        }
    }

    public void OnLeave()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(0);
    }
}
