using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UITest : MonoBehaviour
{
    GameControls gameControls;
    private void Awake()
    {
        gameControls = new GameControls();

        gameControls.Common.Escape.performed += Escape;
   
    }

    void Escape(InputAction.CallbackContext context)
    {
        Debug.Log(context);
    }

    private void OnEnable()
    {
        gameControls.Common.Enable();
    }

    private void OnDisable()
    {
        gameControls.Common.Disable();
    }
}
