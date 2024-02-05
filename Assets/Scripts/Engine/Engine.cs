using System;
using UnityEngine;

public class Engine : MonoBehaviour
{
    [Serializable]
    public struct PositionThrusters
    {
        public int[] Right;
        public int[] Left;
        public int[] Up;
        public int[] Down;
        public int[] Forward;
        public int[] Backward;
    }

    private GameInput _input;

    [SerializeField] private Thruster[] _thrusters;
    [SerializeField] private PositionThrusters _positionThrusters;

    private void Awake()
    {
        foreach (Thruster thruster in _thrusters)
        {
            TryGetComponent<Rigidbody>(out thruster.Body);
        }

        _input = new GameInput();

        _input.Engine.TranslateX.performed += context => Ignite(context.ReadValue<float>(), _positionThrusters.Right, _positionThrusters.Left);
        _input.Engine.TranslateX.canceled += context => Cutoff(_positionThrusters.Right, _positionThrusters.Left);

        _input.Engine.TranslateY.performed += context => Ignite(context.ReadValue<float>(), _positionThrusters.Up, _positionThrusters.Down);
        _input.Engine.TranslateY.canceled += context => Cutoff(_positionThrusters.Up, _positionThrusters.Down);

        _input.Engine.TranslateZ.performed += context => Ignite(context.ReadValue<float>(), _positionThrusters.Forward, _positionThrusters.Backward);
        _input.Engine.TranslateZ.canceled += context => Cutoff(_positionThrusters.Forward, _positionThrusters.Backward);
    }

    private void OnEnable()
    {
        _input.Engine.Enable();
    }

    private void OnDisable()
    {
        _input.Engine.Disable();
    }

    private void Ignite(float value, int[] positiveThrusterIds, int[] negativeThrusterIds)
    {
        int[] affectedThrusterIds;

        if (value > 0)
        {
            affectedThrusterIds = positiveThrusterIds;
        }
        else
        {
            affectedThrusterIds = negativeThrusterIds;
        }
        
        foreach (int id in affectedThrusterIds)
        {
            _thrusters[id].IgniteVfx();
            _thrusters[id].IgniteSfx();
            _thrusters[id].Ignite();
        }
    }

    private void Cutoff(int[] positiveThrusterIds, int[] negativeThrusterIds)
    {
        foreach (int id in positiveThrusterIds)
        {
            _thrusters[id].CutoffVfx();
            _thrusters[id].CutoffSfx();
            _thrusters[id].Cutoff();
        }

        foreach (int id in negativeThrusterIds)
        {
            _thrusters[id].CutoffVfx();
            _thrusters[id].CutoffSfx();
            _thrusters[id].Cutoff();
        }
    }
}
