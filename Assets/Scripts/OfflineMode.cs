using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflineMode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.OfflineMode = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
