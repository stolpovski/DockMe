using Cinemachine;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviourPunCallbacks
{
    private void Awake()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        
        if (!photonView.IsMine)
        {
            GetComponent<CinemachineFreeLook>().Priority = 0;
        }
    }
}
