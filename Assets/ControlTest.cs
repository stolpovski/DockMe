using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlTest : MonoBehaviour
{
    GameControls gameControls;

    private void Awake()
    {
        gameControls = new GameControls();

        gameControls.Car.Forward.performed += Forward;
        gameControls.Car.Forward.canceled += Stop;
    }
    
    

    void Forward(InputAction.CallbackContext context)
    {
        Debug.Log(context);
    }

    void Stop(InputAction.CallbackContext context)
    {
        Debug.Log(context);
    }

    private void OnEnable()
    {
        gameControls.Car.Enable();
    }

    private void OnDisable()
    {
        gameControls.Car.Disable();
    }
}
