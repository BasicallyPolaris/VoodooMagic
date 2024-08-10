using UnityEngine;
using VoodooMagic.Scripts.Constants;

namespace VoodooMagic.Scripts.UI
{
    public class UIInteractor : MonoBehaviour
    {
        private UIInteractable highlightedUIElement;

        public void OnUpdate(Transform raycastSource, RaycastDirection raycastDirection)
        {
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