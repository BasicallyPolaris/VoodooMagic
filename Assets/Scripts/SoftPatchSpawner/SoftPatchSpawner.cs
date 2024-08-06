using UnityEngine;
using UnityEngine.InputSystem;

namespace VoodooMagic.Scripts.SoftPatchSpawner
{
    public class SoftPatchSpawner : BaseSoftPatchSpawner
    {
        [Header("Actions")]
        [SerializeField] private InputActionReference spawnAction;
        [SerializeField] private InputActionReference deleteAction;

        protected override void OnEnable()
        {
            base.OnEnable();
            spawnAction.action.performed += OnSpawnPerformed;
            spawnAction.action.canceled += OnSpawnCanceled;
            spawnAction.action.Enable();

            deleteAction.action.performed += OnDeletePerformed;
            deleteAction.action.canceled += OnDeleteCanceled;
            deleteAction.action.Enable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            spawnAction.action.performed -= OnSpawnPerformed;
            spawnAction.action.canceled -= OnSpawnCanceled;
            spawnAction.action.Disable();

            deleteAction.action.performed -= OnDeletePerformed;
            deleteAction.action.canceled -= OnDeleteCanceled;
            deleteAction.action.Disable();
        }

        protected override void CheckInputs()
        {
            // Input checking is handled by Unity's Input System events
        }

        private void OnSpawnPerformed(InputAction.CallbackContext context)
        {
            SpawnSoftPatch();
        }

        private void OnSpawnCanceled(InputAction.CallbackContext context)
        {
            isSpawnHeld = false;
        }

        private void OnDeletePerformed(InputAction.CallbackContext context)
        {
            DeleteHighlightedObject();
        }

        private void OnDeleteCanceled(InputAction.CallbackContext context)
        {
            isDeleteHeld = false;
        }
    }
}