using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class Spacecraft : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Engine[] _engines;

    [SerializeField]
    private int[] _translateForwardEngines;

    private void Awake()
    {
        foreach (Engine engine in _engines)
        {
            TryGetComponent<Rigidbody>(out engine.RB);
        }
    }

    public void OnTranslateForward(InputAction.CallbackContext context)
    {
        if (!photonView.IsMine)
        {
            return;
        }

        if (context.started)
        {
            photonView.RPC("RunEngines", RpcTarget.All, _translateForwardEngines);
        }

        if (context.canceled)
        {
            photonView.RPC("StopEngines", RpcTarget.All, _translateForwardEngines);
        }
    }

    [PunRPC]
    private void RunEngines(int[] engines)
    {
        foreach (int i in engines)
        {
            _engines[i].Run();
        }
    }

    [PunRPC]
    private void StopEngines(int[] engines)
    {
        foreach (int i in engines)
        {
            _engines[i].Stop();
        }
    }
}
