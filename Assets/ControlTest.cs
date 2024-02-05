using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlTest : MonoBehaviour
{
    GameControls gameControls;
    double pitchTime;

    private void Awake()
    {
        gameControls = new GameControls();

        //gameControls.Car.Forward.performed += Forward;
        //gameControls.Car.Forward.canceled += Stop;

        gameControls.Car.Pitch.performed += StartPitch;
        gameControls.Car.Pitch.canceled += StopPitch;
    }

    void StartPitch(InputAction.CallbackContext context)
    {
        Debug.Log(context);
    }

    void StopPitch(InputAction.CallbackContext context)
    {
        Debug.Log(context);
    }





    void Forward(InputAction.CallbackContext context)
    {


        
        Debug.Log(context);
        pitchTime = context.time;
    }

    void Stop(InputAction.CallbackContext context)
    {
        Debug.Log(context);

        double t = context.time - pitchTime;
        Debug.Log(t);

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
