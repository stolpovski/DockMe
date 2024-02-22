using Photon.Pun;
using System;
using UnityEngine;

namespace SkyDocker
{
    public class Engine : MonoBehaviourPunCallbacks
    {
        private Propellant _propellant;
        
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

        [Serializable]
        public struct RotationThrusters
        {
            public int[] PitchUp;
            public int[] PitchDown;
            public int[] YawRight;
            public int[] YawLeft;
            public int[] RollRight;
            public int[] RollLeft;
        }

        [SerializeField] private Thruster[] _thrusters;
        [SerializeField] private PositionThrusters _positionThrusters;
        [SerializeField] private RotationThrusters _rotationThrusters;

        private GameInput _input;

        private void Awake()
        {
            _propellant = GetComponent<Propellant>();
            int i = 0;
            foreach (Thruster thruster in _thrusters)
            {
                thruster.Id = i++;
                TryGetComponent(out thruster.Engine);
                TryGetComponent<Rigidbody>(out thruster.Body);
                TryGetComponent<Propellant>(out thruster.Propellant);
            }

            _input = new GameInput();

            _input.Engine.TranslateForward.performed += context => IgniteThrusters(_positionThrusters.Forward);
            _input.Engine.TranslateForward.canceled += context => CutoffThrusters(_positionThrusters.Forward);
            _input.Engine.TranslateBackward.performed += context => IgniteThrusters(_positionThrusters.Backward);
            _input.Engine.TranslateBackward.canceled += context => CutoffThrusters(_positionThrusters.Backward);

            _input.Engine.TranslateRight.performed += context => IgniteThrusters(_positionThrusters.Right);
            _input.Engine.TranslateRight.canceled += context => CutoffThrusters(_positionThrusters.Right);
            _input.Engine.TranslateLeft.performed += context => IgniteThrusters(_positionThrusters.Left);
            _input.Engine.TranslateLeft.canceled += context => CutoffThrusters(_positionThrusters.Left);

            _input.Engine.TranslateUp.performed += context => IgniteThrusters(_positionThrusters.Up);
            _input.Engine.TranslateUp.canceled += context => CutoffThrusters(_positionThrusters.Up);
            _input.Engine.TranslateDown.performed += context => IgniteThrusters(_positionThrusters.Down);
            _input.Engine.TranslateDown.canceled += context => CutoffThrusters(_positionThrusters.Down);

            _input.Engine.PitchUp.performed += context => IgniteThrusters(_rotationThrusters.PitchUp);
            _input.Engine.PitchUp.canceled += context => CutoffThrusters(_rotationThrusters.PitchUp);
            _input.Engine.PitchDown.performed += context => IgniteThrusters(_rotationThrusters.PitchDown);
            _input.Engine.PitchDown.canceled += context => CutoffThrusters(_rotationThrusters.PitchDown);

            _input.Engine.YawRight.performed += context => IgniteThrusters(_rotationThrusters.YawRight);
            _input.Engine.YawRight.canceled += context => CutoffThrusters(_rotationThrusters.YawRight);
            _input.Engine.YawLeft.performed += context => IgniteThrusters(_rotationThrusters.YawLeft);
            _input.Engine.YawLeft.canceled += context => CutoffThrusters(_rotationThrusters.YawLeft);

            _input.Engine.RollRight.performed += context => IgniteThrusters(_rotationThrusters.RollRight);
            _input.Engine.RollRight.canceled += context => CutoffThrusters(_rotationThrusters.RollRight);
            _input.Engine.RollLeft.performed += context => IgniteThrusters(_rotationThrusters.RollLeft);
            _input.Engine.RollLeft.canceled += context => CutoffThrusters(_rotationThrusters.RollLeft);
        }

        public override void OnEnable()
        {
            base.OnEnable();
            _input.Engine.Enable();
        }

        public override void OnDisable()
        {
            base.OnDisable();
            _input.Engine.Disable();
        }

        private void IgniteThrusters(int[] ids)
        {
            if (!photonView.IsMine || _propellant.IsEmpty)
            {
                return;
            }

            foreach (int id in ids)
            {
                _thrusters[id].Ignite();
            }

        }

        public void RpcIgniteVfx(int id)
        {
            photonView.RPC("RPC_IgniteThruster", RpcTarget.Others, id);
        }

        public void RpcCutoffVfx(int id)
        {
            photonView.RPC("RPC_CutoffThruster", RpcTarget.Others, id);
        }

        [PunRPC]
        private void RPC_IgniteThruster(int id)
        {
            _thrusters[id].IgniteVfx();
        }

        [PunRPC]
        private void RPC_CutoffThruster(int id)
        {
            _thrusters[id].CutoffVfx();
        }

        private void CutoffThrusters(int[] ids)
        {
            if (!photonView.IsMine || _propellant.IsEmpty)
            {
                return;
            }
            
            foreach (int id in ids)
            {
                _thrusters[id].Cutoff();
            }

        
        }
    }
}
