using Photon.Pun;
using System;
using UnityEngine;

namespace DockMe
{
    public class Engine : MonoBehaviourPunCallbacks
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

        private GameInput _input;
        private Rigidbody rb;

        private Spacecraft craft;

        private void Awake()
        {
            craft = GetComponent<Spacecraft>();
            rb = GetComponent<Rigidbody>();
            foreach (Thruster thruster in _thrusters)
            {
                TryGetComponent<Rigidbody>(out thruster.Body);
                TryGetComponent<Spacecraft>(out thruster.Craft);
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
            if (!photonView.IsMine || craft.Propellant < 0)
            {
                return;
            }

            foreach (int id in ids)
            {
                _thrusters[id].Ignite();
                _thrusters[id].IgniteVfx();
                _thrusters[id].IgniteSfx();
            }

            photonView.RPC("RPC_IgniteThrusters", RpcTarget.Others, ids);
        }

        [PunRPC]
        private void RPC_IgniteThrusters(int[] ids)
        {
            foreach (int id in ids)
            {
                _thrusters[id].IgniteVfx();
            }
        }

        [PunRPC]
        private void RPC_CutoffThrusters(int[] ids)
        {
            foreach (int id in ids)
            {
                _thrusters[id].CutoffVfx();
            }
        }

        private void CutoffThrusters(int[] ids)
        {
            if (!photonView.IsMine || craft.Propellant < 0)
            {
                return;
            }
            
            foreach (int id in ids)
            {
                _thrusters[id].Cutoff();
                _thrusters[id].CutoffVfx();
                _thrusters[id].CutoffSfx();
            }

            photonView.RPC("RPC_CutoffThrusters", RpcTarget.Others, ids);
        }
    }
}
