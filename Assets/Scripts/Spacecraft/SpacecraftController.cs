using Cinemachine;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacecraftController : MonoBehaviourPunCallbacks
{
    private GameInput _gameInput;
    //[SerializeField] private CinemachineFreeLook _lookCamera;
    [SerializeField] private CinemachineVirtualCamera _dockCamera;
    [SerializeField] private GameObject _dockPanel;
    [SerializeField] private GameObject _flashlight;
    private void Awake()
    {
        _gameInput = new GameInput();

        _gameInput.Spacecraft.ChangeView.performed += context => ChangeView();
        _gameInput.Spacecraft.ToggleFlashlight.performed += context => ToggleFlashlight();
    }

    public override void OnEnable()
    {
        _gameInput.Spacecraft.Enable();
    }

    public override void OnDisable()
    {
        _gameInput.Spacecraft.Disable();
    }

    private void ChangeView()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        if (_dockCamera.Priority == 0)
        {
            _dockCamera.Priority = 1;
            _dockPanel.SetActive(true);
        }
        else
        {
            _dockCamera.Priority = 0;
            _dockPanel.SetActive(false);
        }
        

    }

    private void ToggleFlashlight()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        if (!_flashlight.activeSelf)
        {
            _flashlight.SetActive(true );
        }
        else
        {
            _flashlight.SetActive(false);
        }
    }
}
