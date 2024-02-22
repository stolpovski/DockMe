using SkyDocker;
using UnityEngine;
using UnityEngine.InputSystem;

public class EngineController : MonoBehaviour
{
    private GameInput gameInput;
    [SerializeField] private Engine0[] engines;

    private void Awake()
    {
        gameInput = new GameInput();

        //gameInput.Engine.TranslateForward.performed += TranslateForward;
    }

    private void TranslateForward(InputAction.CallbackContext context)
    {
        Debug.Log(context);
    }

    private void OnEnable()
    {
        gameInput.Engine.Enable();
    }

    private void OnDisable()
    {
        gameInput.Engine.Disable();
    }
}
