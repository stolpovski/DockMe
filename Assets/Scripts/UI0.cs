using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI0 : MonoBehaviourPunCallbacks
{
    public Button btn;
    public void OnLeave()
    {
        btn.interactable = false;
        PhotonNetwork.Disconnect();
        PhotonNetwork.LoadLevel(0);
    }
}
