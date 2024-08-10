using UnityEngine;
using VoodooMagic.Scripts.Constants;

namespace VoodooMagic.Scripts.UI
{
    public class UIInteractor : MonoBehaviour
    {
        private UIInteractable highlightedUIElement;
        private bool isActive = false;

        public void OnUpdate(Transform raycastSource, RaycastDirection raycastDirection)
        {
            if (!isActive) return;

            RaycastHit hit;

            if (Physics.Raycast(raycastSource.position, Util.GetRayDirection(raycastSource, raycastDirection), out hit))
            {
                if (hit.collider.gameObject.TryGetComponent(out UIInteractable interactable))
                {
                    if (highlightedUIElement != interactable)
                    {
                        RemoveHighlight();
                        highlightedUIElement = interactable;
                        highlightedUIElement.highlight();
                    }
                    return;
                }
            }

            RemoveHighlight();
        }

        public void OnPinch()
        {
            if (highlightedUIElement != null) highlightedUIElement.OnPinch();
        }

        public void OnGrab()
        {
            if (highlightedUIElement != null) highlightedUIElement.OnGrab();
        }

        public void SetIsActive(bool value)
        {
            if (!isActive) RemoveHighlight();
            isActive = value;
        }

        private void RemoveHighlight()
        {
            if (highlightedUIElement != null)
            {
                highlightedUIElement.removeHighlight();
                highlightedUIElement = null;
            }
        }
    }
}