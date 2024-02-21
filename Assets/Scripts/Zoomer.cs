using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DockMe
{
    public class Zoomer : MonoBehaviour
    {
        private GameInput _input;
        private CinemachineFreeLook _cam;

        [SerializeField] private float _delta = 1f;
        [SerializeField] private float _minDistance = 5f;
        [SerializeField] private float _maxDistance = 100f;

        private void Awake()
        {
            _cam = GetComponent<CinemachineFreeLook>();
            
            _input = new GameInput();
            _input.Cam.Zoom.performed += Zoom;
        }

        private void OnEnable()
        {
            _input.Cam.Enable();
        }

        private void OnDisable()
        {
            _input.Cam.Disable();
        }

        private void Zoom(InputAction.CallbackContext context)
        {
            float value = context.ReadValue<float>();

            _cam.m_Orbits[0].m_Height = Mathf.Clamp(_cam.m_Orbits[0].m_Height - value * _delta, _minDistance, _maxDistance);
            _cam.m_Orbits[1].m_Radius = Mathf.Clamp(_cam.m_Orbits[1].m_Radius - value * _delta, _minDistance, _maxDistance);
            _cam.m_Orbits[2].m_Height = Mathf.Clamp(_cam.m_Orbits[2].m_Height + value * _delta, -_maxDistance, -_minDistance);
        }
    }
}
