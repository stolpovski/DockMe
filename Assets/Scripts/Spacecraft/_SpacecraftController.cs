using Cinemachine;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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
    [SerializeField] private TMP_Text _val1;
    [SerializeField] private TMP_Text _val2;
    [SerializeField] private TMP_Text _val3;
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
        float distance = Vector3.Distance(_SSVProbe.transform.position, new Vector3(0.01f, 0.00f, -10.28f));
        Vector3 angVel = _body.angularVelocity;
        _val2.SetText(String.Format(new CultureInfo("en-US"),
            "{0:F2}\n\n{1:F3}\n{2:F3}\n{3:F3}",
            distance,
            angVel.x * 57.2958,
            angVel.y * 57.2958,
            angVel.z * 57.2958/*,
            _body.velocity.x,
            _body.velocity.y,
            _body.velocity.z*/
        ));

        Vector3 _angDelta = transform.localEulerAngles;
        

        float propellant = 45.0f;
        _val1.SetText(String.Format(new CultureInfo("en-US"),
            //"{0:F1}\n{1:F1}\n\n{2:F1}\n{3:F1}\n{4:F1}",
            "{0:F2}\n\n{1:F1}\n{2:F1}\n{3:F1}",
            _body.velocity.magnitude,
            _angDelta.x,
            _angDelta.y,
            _angDelta.z
        ));


        _val3.SetText(String.Format(new CultureInfo("en-US"),
            //"{0:F1}\n{1:F1}\n\n{2:F1}\n{3:F1}\n{4:F1}",
            "{0:F2}\n\n{1:F3}\n{2:F3}\n{3:F3}",
            propellant,
            _body.velocity.x,
            _body.velocity.y,
            _body.velocity.z
        ));



        //Debug.Log(transform.localRotation.x);
        //Debug.Log(UnityEditor.TransformUtils.GetInspectorRotation(gameObject.transform).x);
    }
}
