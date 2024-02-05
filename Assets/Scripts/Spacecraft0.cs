using Photon.Pun;
using Photon.Voice.Unity;
using UnityEngine;
using UnityEngine.InputSystem;

public class Spacecraft0 : MonoBehaviourPunCallbacks
{
    public Recorder recorder;
    public AudioSource radioIntro;
    public AudioSource radioOutro;
    [SerializeField]
    public GameObject PlayerUiPrefab;
    
    [SerializeField]
    private Engine0[] _engines;

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

        foreach (Engine0 engine in _engines)
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

        if (PlayerUiPrefab != null)
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
    private void StartEnginesVfx(int[] engines)
    {
        foreach (int i in engines)
        {
            _engines[i].StartVFX();
        }
    }

    [PunRPC]
    private void StopEnginesVfx(int[] engines)
    {
        foreach (int i in engines)
        {
            _engines[i].StopVFX();
        }
    }

    private void StartEnginesSfx(int[] engines)
    {
        foreach (int i in engines)
        {
            _engines[i].StartSFX();
        }
    }

    private void StopEnginesSfx(int[] engines)
    {
        foreach (int i in engines)
        {
            _engines[i].StopSFX();
        }
    }

    private void StartBurning(int[] engines)
    {
        foreach (int i in engines)
        {
            if (_engines[i].isActiveAndEnabled) _engines[i].StartBurning();
        }
    }

    private void StopBurning(int[] engines)
    {
        foreach (int i in engines)
        {
            _engines[i].StopBurning();
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
            StartBurning(engines);
            StartEnginesVfx(engines);
            StartEnginesSfx(engines);
            // StartEngines(engines);
            photonView.RPC("StartEnginesVfx", RpcTarget.Others, engines);
        }

        if (context.canceled)
        {
            StopBurning(engines);
            StopEnginesVfx(engines);
            StopEnginesSfx(engines);
            photonView.RPC("StopEnginesVfx", RpcTarget.Others, engines);
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
            BeepIntro();
            photonView.RPC("BeepIntro", RpcTarget.Others);
            recorder.TransmitEnabled = true;
            
        }

        if (context.canceled)
        {
            BeepOutro();
            photonView.RPC("BeepOutro", RpcTarget.Others);
            recorder.TransmitEnabled = false;
            
        }
    }

    [PunRPC]
    private void BeepIntro()
    {
        radioOutro.Stop();
        radioIntro.Play();
    }

    [PunRPC]
    private void BeepOutro()
    {
        radioIntro.Stop();
        radioOutro.Play();
    }
}
