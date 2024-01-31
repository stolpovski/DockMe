using Cinemachine;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;

public class SpacecraftController : MonoBehaviourPunCallbacks
{
    private Rigidbody _body;
    private GameInput _gameInput;
    //[SerializeField] private CinemachineFreeLook _lookCamera;
    [SerializeField] private CinemachineVirtualCamera _dockCamera;
    [SerializeField] private GameObject _dockPanel;
    [SerializeField] private GameObject _flashlight;
    [SerializeField] private TMP_Text _angVelVal;
    [SerializeField] private TMP_Text _angDeltaVal;
    [SerializeField] private GameObject _SSVProbe;
    private void Awake()
    {
        _dockPanel.SetActive(false);
        _body = GetComponent<Rigidbody>();
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

    private void Update()
    {
        Vector3 angVel = _body.angularVelocity;
        _angVelVal.SetText(String.Format("{0:F4}\n{1:F4}\n{2:F4}", angVel.z * 57.2958, angVel.y * 57.2958, angVel.x * 57.2958));

        Vector3 _angDelta = transform.rotation.eulerAngles;
        float distance = Vector3.Distance(_SSVProbe.transform.position, new Vector3(0.01f, 0.00f, -10.28f));



        _angDeltaVal.SetText(String.Format(
            "{0:F2}\n{1:F2}\n\n{2:F1}\n{3:F1}\n{4:F1}",
            _body.velocity.magnitude,
            distance,
            _angDelta.x > 180 ? 360 - _angDelta.x : -_angDelta.x, 
            _angDelta.y > 180 ? _angDelta.y - 360 : _angDelta.y,
            _angDelta.z > 180 ? 360 - _angDelta.z : -_angDelta.z
        ));

        //Debug.Log(_angDelta);
    }
}
