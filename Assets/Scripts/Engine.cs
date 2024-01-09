using Photon.Pun;
using UnityEngine;

public class Engine : MonoBehaviourPunCallbacks
{
    public Thruster[] thrusters;
    
    //Rigidbody rb;
    float force;

    int[] translateForwardIds = {0};
    int[] translateBackIds = {2};
    int[] translateLeftIds = {3};
    int[] translateRightIds = {1};
    int[] translateUpIds = {4};
    int[] translateDownIds = {5};

    private void Awake()
    {
        foreach (Thruster thr in thrusters)
        {
            TryGetComponent<Rigidbody>(out thr.body);
        }
        
        //rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (photonView.IsMine)
        {
            

            if (Input.GetKeyDown(KeyCode.W))
            {
                photonView.RPC("Burn", RpcTarget.All, translateForwardIds);
            }

            if (Input.GetKeyUp(KeyCode.W))
            {
                photonView.RPC("Stop", RpcTarget.All, translateForwardIds);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                photonView.RPC("Burn", RpcTarget.All, translateBackIds);
            }

            if (Input.GetKeyUp(KeyCode.S))
            {
                photonView.RPC("Stop", RpcTarget.All, translateBackIds);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                photonView.RPC("Burn", RpcTarget.All, translateLeftIds);
            }

            if (Input.GetKeyUp(KeyCode.A))
            {
                photonView.RPC("Stop", RpcTarget.All, translateLeftIds);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                photonView.RPC("Burn", RpcTarget.All, translateRightIds);
            }

            if (Input.GetKeyUp(KeyCode.D))
            {
                photonView.RPC("Stop", RpcTarget.All, translateRightIds);
            }



            if (Input.GetKeyDown(KeyCode.E))
            {
                photonView.RPC("Burn", RpcTarget.All, translateUpIds);
            }

            if (Input.GetKeyUp(KeyCode.E))
            {
                photonView.RPC("Stop", RpcTarget.All, translateUpIds);
            }


            if (Input.GetKeyDown(KeyCode.Q))
            {
                photonView.RPC("Burn", RpcTarget.All, translateDownIds);
            }

            if (Input.GetKeyUp(KeyCode.Q))
            {
                photonView.RPC("Stop", RpcTarget.All, translateDownIds);
            }



        }
    }

    [PunRPC]
    private void Burn(int[] ids)
    {
        foreach(int id in ids)
        {
            thrusters[id].Burn();
        }
        
    }

    [PunRPC]
    private void Stop(int[] ids)
    {
        foreach (int id in ids)
        {
            thrusters[id].Stop();
        }
    }
}
