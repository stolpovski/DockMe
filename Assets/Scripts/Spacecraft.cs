using Photon.Pun;
using Photon.Voice.Unity;
using UnityEngine;
using UnityEngine.InputSystem;

public class Spacecraft : MonoBehaviourPunCallbacks
{
    public Recorder recorder;
    public AudioSource radioIntro;
    public AudioSource radioOutro;
    [SerializeField]
    public GameObject PlayerUiPrefab;
    
    [SerializeField]
    private Engine[] _engines;

    [SerializeField]
    private PositionEngines _positionEngines;

    [SerializeField]
    private RotationEngines _rotationEngines;

    private PlayerInput _playerInput;

    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponentInChildren<Renderer>();
        _playerInput = GetComponent<PlayerInput>();
        recorder = GameObject.Find("Recorder").GetComponent<Recorder>();

        foreach (Engine engine in _engines)
        {
            TryGetComponent<Rigidbody>(out engine.RB);
        }
    }

    private void Start()
    {
        if (photonView.IsMine)
        {
            RandomizeColor();
        }

        if (PlayerUiPrefab != null && !photonView.IsMine)
        {
            GameObject _uiGo = Instantiate(PlayerUiPrefab);
            _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
        }
        else
        {
            Debug.LogWarning("<Color=Red><a>Missing</a></Color> PlayerUiPrefab reference on player Prefab.", this);
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
    private void ChangeColor(float R, float G, float B)
    {
        _renderer.materials[1].SetColor("_Color", new Color(R, G, B));
    }

    public void OnChangeColor(InputAction.CallbackContext context)
    {
        if (photonView.IsMine && context.started)
        {
            RandomizeColor();
        }
            
    }

    private void RandomizeColor()
    {
        photonView.RPC("ChangeColor", RpcTarget.AllBuffered, Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }

    public void OnRadio(InputAction.CallbackContext context)
    {
        if (!photonView.IsMine)
        {
            return;
        }
        
        if (context.started)
        {
            recorder.TransmitEnabled = true;
            photonView.RPC("Beep", RpcTarget.Others, true);
        }

        if (context.canceled)
        {
            recorder.TransmitEnabled = false;
            photonView.RPC("Beep", RpcTarget.Others, false);
        }
    }

    [PunRPC]
    private void Beep(bool isIntro)
    {
        if (isIntro)
        {
            radioOutro.Stop();
            radioIntro.Play();
        }
        else
        {
            radioIntro.Stop();
            radioOutro.Play();
        }
    }
    
}
