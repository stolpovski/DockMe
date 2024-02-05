using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField] private string[] _launchCommands;
    const string playerNamePrefKey = "PlayerName";
    const string defaultName = "UFO";

    [SerializeField]
    TMP_Text _label;

    [SerializeField]
    Button _connectBtn;

    [SerializeField]
    TMP_InputField _playerName;

    [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
    [SerializeField]
    private byte maxPlayersPerRoom = 4;

    private void Awake()
    {
        _label.enabled = false;
        _connectBtn.interactable = false;
        //PlayerPrefs.DeleteKey(playerNamePrefKey);

        if (PlayerPrefs.HasKey(playerNamePrefKey))
        {
            _connectBtn.interactable = true;
            _playerName.text = PlayerPrefs.GetString(playerNamePrefKey);
        }
        _playerName.Select();
    }

    IEnumerator ConnectingCoroutine()
    {
        _label.SetText("");
        _label.enabled = true;
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        foreach (string cmd in _launchCommands)
        {
            _label.SetText(cmd);
            yield return new WaitForSeconds(Random.Range(0.2f, 0.4f));

            
        }

        //yield on a new YieldInstruction that waits for 5 seconds.
        

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }


    public void OnConnect()
    {
        _playerName.interactable = false;
        _connectBtn.interactable = false;

        

        StartCoroutine(ConnectingCoroutine());

        Connect();

        
    }

    private void Connect()
    {
        
        PhotonNetwork.ConnectUsingSettings();

    }

    public void SetPlayerName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            _connectBtn.interactable = false;
            return;
        }
        
        PhotonNetwork.NickName = name;
        PlayerPrefs.SetString(playerNamePrefKey, name);
        Debug.Log("SetPlayerName");
        _connectBtn.interactable = true;
    }

    public override void OnConnectedToMaster()
    {
        //PhotonNetwork.Disconnect();return;
        //PhotonNetwork.NickName = "Player_" + Random.Range(0, 10);
        Debug.Log("OnConnectedToMaster() was called by PUN");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("OnDisconnected() was called by PUN with reason {0}", cause);
        _connectBtn.interactable = true;
        //_label.SetText("Connection failed");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

        // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
        PhotonNetwork.LoadLevel("Room");
    }
}
