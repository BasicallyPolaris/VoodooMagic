using UnityEngine;
using VoodooMagic.Scripts.Constants;
using VoodooMagic.Scripts.RayInteraction.Interactables;

namespace VoodooMagic.Scripts.RayInteraction
{
    public class RayInteractor : MonoBehaviour
    {
        private RayInteractable focusedElement;
        private bool isActive = false;

        public void OnUpdate(Transform raycastSource, RaycastDirection raycastDirection)
        {
            if (!isActive) return;

            RaycastHit hit;

            Debug.DrawRay(raycastSource.position, Util.GetRayDirection(raycastSource, raycastDirection) * 10, Color.red);

            if (Physics.Raycast(raycastSource.position, Util.GetRayDirection(raycastSource, raycastDirection), out hit))
            {
                if (hit.collider.gameObject.TryGetComponent(out RayInteractable interactable))
                {
                    if (focusedElement != interactable)
                    {
                        RemoveHighlight();
                        focusedElement = interactable;
                        focusedElement.highlight();
                    }
                    return;
                }
            }

            RemoveHighlight();
        }

        public void OnPinch()
        {
            if (focusedElement != null) focusedElement.OnPinch();
        }

        public void OnGrab()
        {
            if (focusedElement != null) focusedElement.OnGrab();
        }

        public void SetIsActive(bool value)
        {
            if (!isActive) RemoveHighlight();
            isActive = value;
        }

        private void RemoveHighlight()
        {
            if (focusedElement != null)
            {
                focusedElement.removeHighlight();
                focusedElement = null;
            }
        }
    }
}