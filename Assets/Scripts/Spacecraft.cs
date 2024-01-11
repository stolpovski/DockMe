using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class Spacecraft : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Engine[] _engines;

    [SerializeField]
    private PositionEngines _positionEngines;

    [SerializeField]
    private RotationEngines _rotationEngines;

    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();

        foreach (Engine engine in _engines)
        {
            TryGetComponent<Rigidbody>(out engine.RB);
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

    

    private void HandleEngines(InputAction.CallbackContext context, int[] engines)
    {
        if (!photonView.IsMine)
        {
            return;
        }

        if (context.started)
        {
            photonView.RPC("RunEngines", RpcTarget.All, engines);
        }

        if (context.canceled)
        {
            photonView.RPC("StopEngines", RpcTarget.All, engines);
        }
    }
    
    public void OnTranslateForward(InputAction.CallbackContext context)
    {
        HandleEngines(context, _positionEngines.Forward);
    }

    public void OnTranslateBackward(InputAction.CallbackContext context)
    {
        HandleEngines(context, _positionEngines.Backward);
    }

    public void OnTranslateRight(InputAction.CallbackContext context)
    {
        HandleEngines(context, _positionEngines.Right);
    }

    public void OnTranslateLeft(InputAction.CallbackContext context)
    {
        HandleEngines(context, _positionEngines.Left);
    }

    public void OnTranslateUp(InputAction.CallbackContext context)
    {
        HandleEngines(context, _positionEngines.Up);
    }

    public void OnTranslateDown(InputAction.CallbackContext context)
    {
        HandleEngines(context, _positionEngines.Down);
    }

    public void OnPitchUp(InputAction.CallbackContext context)
    {
        HandleEngines(context, _rotationEngines.PitchUp);
        Debug.Log("pitch up");
    }

    public void OnPitchDown(InputAction.CallbackContext context)
    {
        HandleEngines(context, _rotationEngines.PitchDown);
    }

    public void OnYawRight(InputAction.CallbackContext context)
    {
        HandleEngines(context, _rotationEngines.YawRight);
    }

    public void OnYawLeft(InputAction.CallbackContext context)
    {
        HandleEngines(context, _rotationEngines.YawLeft);
    }

    public void OnRollRight(InputAction.CallbackContext context)
    {
        HandleEngines(context, _rotationEngines.RollRight);
    }

    public void OnRollLeft(InputAction.CallbackContext context)
    {
        HandleEngines(context, _rotationEngines.RollLeft);
    }

    public void OnRotationMode(InputAction.CallbackContext context)
    {
        if (photonView.IsMine && context.performed)
        {
            _playerInput.SwitchCurrentActionMap("Rotation");
        }
    }

    public void OnPositionMode(InputAction.CallbackContext context)
    {
        if (photonView.IsMine && context.performed)
        {
            _playerInput.SwitchCurrentActionMap("Position");
        }
    }

    [PunRPC]
    private void RandomColor(float R, float G, float B)
    {
        GetComponentInChildren<Renderer>().materials[1].SetColor("_Color", new Color(R, G, B));
    }

    public void OnChangeColor(InputAction.CallbackContext context)
    {
        if (photonView.IsMine && context.started)
        {
            photonView.RPC("RandomColor", RpcTarget.All, Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        }
            
    }
}
