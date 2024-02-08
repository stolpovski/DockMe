using Photon.Pun;
using System;
using UnityEngine;

namespace DockMe
{
    public class Engine : MonoBehaviourPunCallbacks
    {
        public Propellant Propellant;
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

        private GameInput _input;

        private void Awake()
        {
            Propellant = GetComponent<Propellant>();
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

            
        }

        override public void OnEnable()
        {
            _input.Engine.Enable();
        }

        override public void OnDisable()
        {
            _input.Engine.Disable();
        }

        private void IgniteThrusters(int[] ids)
        {
            if (!photonView.IsMine || Propellant.IsEmpty)
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
            if (!photonView.IsMine || Propellant.IsEmpty)
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
