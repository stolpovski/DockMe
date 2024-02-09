using Photon.Pun;
using Photon.Realtime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace DockMe
{
    public class Lobby : MonoBehaviourPunCallbacks
    {
        private readonly string gameVersion = "1";

        [SerializeField]
        private byte maxPlayersPerRoom = 4;

        [Tooltip("The Ui Panel to let the user enter name, connect and play")]
        [SerializeField]
        private GameObject controlPanel;
        [Tooltip("The UI Label to inform the user that the connection is in progress")]
        [SerializeField]
        private GameObject progressLabel;

        [SerializeField]
        private Button _playButton;

        private GameInput _gameInput;

        private void Awake()
        {
            _gameInput = new GameInput();


            _gameInput.UI.Submit.performed += ctx => { if (_playButton.interactable) _playButton.onClick.Invoke(); };
        }

        bool isConnecting;

        override public void OnEnable()
        {
            base.OnEnable();
            _gameInput.UI.Enable();
        }

        override public void OnDisable()
        {
            base.OnDisable();
            _gameInput.UI.Disable();
        }

        private void Start()
        {
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);
        }

        public void Connect()
        {
            progressLabel.SetActive(true);
            controlPanel.SetActive(false);
            _playButton.interactable = false;
            
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                isConnecting = PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = gameVersion;
            }
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("OnConnectedToMaster() was called by PUN");
            if (isConnecting)
            {
                // #Critical: The first we try to do is to join a potential existing room. If there is, good, else, we'll be called back with OnJoinRandomFailed()
                PhotonNetwork.JoinRandomRoom();
                isConnecting = false;
            }
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.LogWarningFormat("OnDisconnected() was called by PUN with reason {0}", cause);

            progressLabel.SetActive(false);
            controlPanel.SetActive(true);
            _playButton.interactable = true;
            isConnecting = false;
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("OnJoinedRoom() called by PUN. Now this client is in a room.");
            PhotonNetwork.LoadLevel(1);
        }
    }
}
