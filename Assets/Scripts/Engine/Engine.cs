using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Engine : MonoBehaviour
{
    [Serializable]
    public struct PositionThrusters
    {
        public int[] Forward;
        public int[] Backward;
        public int[] Right;
        public int[] Left;
        public int[] Up;
        public int[] Down;
    }

    [SerializeField] private Thruster[] _thrusters;
    [SerializeField] private PositionThrusters _positionThrusters;
    [SerializeField] private bool _isSAS;

    private GameInput _input;
    private bool _wasPositiveTranslationZ;

    private void Awake()
    {
        foreach (Thruster thruster in _thrusters)
        {
            TryGetComponent<Rigidbody>(out thruster.Body);
        }

        _input = new GameInput();

        _input.Engine.TranslateZ.performed += context => Ignite(context, _positionThrusters.Forward, _positionThrusters.Backward, context.ReadValue<float>());
        _input.Engine.TranslateZ.canceled += context => Cutoff(context, _positionThrusters.Forward, _positionThrusters.Backward, _wasPositiveTranslationZ);

        /*_input.Engine.TranslateX.performed += context => Ignite(context, _positionThrusters.Right, _positionThrusters.Left);
        _input.Engine.TranslateX.canceled += context => Cutoff(context, _positionThrusters.Right.Concat(_positionThrusters.Left).ToArray());

        _input.Engine.TranslateY.performed += context => Ignite(context, _positionThrusters.Up, _positionThrusters.Down);
        _input.Engine.TranslateY.canceled += context => Cutoff(context, _positionThrusters.Up.Concat(_positionThrusters.Down).ToArray());*/
    }

    private void OnEnable()
    {
        _input.Engine.Enable();
    }

    private void OnDisable()
    {
        _input.Engine.Disable();
    }

    private void Ignite(InputAction.CallbackContext context, int[] positiveThrusterIds, int[] negativeThrusterIds, float value)
    {
        Debug.Log(context);
        
        int[] affectedThrusterIds;

        if (value > 0)
        {
            affectedThrusterIds = positiveThrusterIds;
            _wasPositiveTranslationZ = true;
        }
        else
        {
            affectedThrusterIds = negativeThrusterIds;
            _wasPositiveTranslationZ = false;
        }
        
        foreach (int id in affectedThrusterIds)
        {
            _thrusters[id].IgniteVfx();
            _thrusters[id].IgniteSfx();
            _thrusters[id].Ignite();
        }
    }

    private void Cutoff(InputAction.CallbackContext context, int[] positiveThrusterIds, int[] negativeThrusterIds, bool wasPositive)
    {
        Debug.Log(context);
        int[] affectedThrusters = wasPositive ? positiveThrusterIds : negativeThrusterIds;
        foreach (int id in affectedThrusters)
        {
            _thrusters[id].CutoffVfx();
            _thrusters[id].CutoffSfx();
            _thrusters[id].Cutoff();
        }

        if (_isSAS)
        {
            Ignite(context, positiveThrusterIds, negativeThrusterIds, wasPositive ? -1 : 1);
        }
    }
}
