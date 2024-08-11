using System;
using UnityEngine;
using VoodooMagic.Scripts.Constants;
using VoodooMagic.Scripts.RayInteraction.Interactables;

namespace VoodooMagic.Scripts.RayInteraction
{
    public class RayInteractor : MonoBehaviour
    {

        [Header("Spawn Location")]
        [Tooltip("Consider taking the root game object of the component the script is on.")]
        [SerializeField] private Transform raycastSource;
        [SerializeField] private RaycastDirection raycastDirection = RaycastDirection.Forward;
        [SerializeField] private GameObject MetaRayHandInteractor;

        private RayInteractable focusedElement;
        private bool isActive = false;

        private void Update()
        {

            if (!isActive) return;

            RaycastHit hit;

            if (Physics.Raycast(raycastSource.position, Util.GetRayDirection(raycastSource, raycastDirection), out hit))
            {
                if (hit.collider.gameObject.TryGetComponent(out RayInteractable interactable))
                {
                    if (focusedElement != interactable)
                    {
                        RemoveHover();
                        focusedElement = interactable;
                        focusedElement.OnHover();
                    }
                    return;
                }
            }

            RemoveHover();
        }

        public void OnPinch()
        {
            if (focusedElement != null) focusedElement.OnPinch();
        }

        public void OnGrab()
        {
            if (focusedElement != null) focusedElement.OnGrab();
        }

        public void SetIsActive(bool active)
        {
            if (!active)
            {
                RemoveHover();
            }
            MetaRayHandInteractor.SetActive(active);
            isActive = active;
        }

        private void RemoveHover()
        {
            if (focusedElement != null)
            {
                focusedElement.RemoveHover();
                focusedElement = null;
            }
        }
    }
}