using UnityEngine;
using UnityEngine.InputSystem;

namespace VoodooMagic.Scripts.Controlls.API
{
    public class ControllerAPI : BaseControllAPI
    {
        [Header("Controller Actions")]
        [SerializeField] private InputActionReference pinchAction;
        [SerializeField] private InputActionReference grabAction;

        private void OnEnable()
        {
            pinchAction.action.performed += OnPinch;
            pinchAction.action.canceled += OnPinchReleased;
            pinchAction.action.Enable();

            grabAction.action.performed += OnGrab;
            grabAction.action.canceled += OnGrabReleased;
            grabAction.action.Enable();
        }

        private void OnDisable()
        {
            pinchAction.action.performed -= OnPinch;
            pinchAction.action.canceled -= OnPinchReleased;
            pinchAction.action.Disable();

            grabAction.action.performed -= OnGrab;
            grabAction.action.canceled -= OnGrabReleased;
            grabAction.action.Disable();
        }

        private void OnPinch(InputAction.CallbackContext context)
        {
            if (!isPinching) OnPinch();
            isPinching = true;
        }

        private void OnPinchReleased(InputAction.CallbackContext context)
        {
            isPinching = false;
        }

        private void OnGrab(InputAction.CallbackContext context)
        {
            if (!isGrabbing) OnGrab();
            isGrabbing = true;
        }

        private void OnGrabReleased(InputAction.CallbackContext context)
        {
            isGrabbing = false;
        }
    }
}