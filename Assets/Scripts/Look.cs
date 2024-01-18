using Cinemachine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Look : MonoBehaviourPunCallbacks
{
    [SerializeField]
    TMP_Text _log;
    [SerializeField]
    private GameObject _menu;
    private bool hasEscaped;
    [SerializeField]
    private float _zoomDelta = 1f;
    CinemachineFreeLook cam;

    float defaultCamX = 300f;
    float defaultCamY = 2f;
    private void Awake()
    {
        _menu.SetActive(false);
        cam = GetComponent<CinemachineFreeLook>();
        Cursor.lockState = CursorLockMode.Locked;
        
        if (!photonView.IsMine)
        {
            cam.Priority = 0;
        }
    }

    public override void OnPlayerEnteredRoom(Player other)
    {
        _log.SetText(other.NickName + " joined");
    }

    public override void OnPlayerLeftRoom(Player other)
    {
        _log.SetText(other.NickName + " left");
    }

    public void OnZoom(InputAction.CallbackContext context)
    {
        if (!photonView.IsMine || !context.started)
        {
            return;
        }

        float value = 0;
        if (context.ReadValue<float>() > 0)
        {
            value = _zoomDelta;
        }
        else
        {
            value = -_zoomDelta;
        }

        cam.m_Orbits[0].m_Height -= value;
        cam.m_Orbits[1].m_Radius -= value;
        cam.m_Orbits[2].m_Height += value;
    }

    public void OnEscape(InputAction.CallbackContext context)
    {
        if (!photonView.IsMine || !context.started)
        {
            return;
        }

        if (!hasEscaped)
        {
            cam.m_XAxis.m_MaxSpeed = 0f;
            cam.m_YAxis.m_MaxSpeed = 0f;
            _zoomDelta = 0;
            Cursor.lockState = CursorLockMode.None;
            _menu.SetActive(true);
        }
        else
        {
            cam.m_XAxis.m_MaxSpeed = defaultCamX;
            cam.m_YAxis.m_MaxSpeed = defaultCamY;
            Cursor.lockState = CursorLockMode.Locked;
            _menu.SetActive(false);
            _zoomDelta = 1;
        }

        hasEscaped = !hasEscaped;
    }
}
