
using UnityEngine;
using UnityEngine.Events;

namespace VoodooMagic.Scripts.RayInteraction.Interactables
{
  /// <summary>
  /// The UI Interactable interface.
  /// </summary>
  public abstract class RayInteractable : MonoBehaviour
  {
    [Header("Actions")]
    [SerializeField] private UnityEvent[] pinchActions;
    [SerializeField] private UnityEvent[] grabActions;
    protected bool wasPressed = false;
    protected bool isHighlighted = false;

    public void OnPinch()
    {
      if (wasPressed) return;
      OnPressed();

      foreach (UnityEvent action in pinchActions)
      {
        action.Invoke();
      }
    }

    public void OnGrab()
    {
      if (wasPressed) return;
      OnPressed();

      foreach (UnityEvent action in grabActions)
      {
        action.Invoke();
      }
    }

    public abstract void OnHover();

    public virtual void RemoveHover() {
      wasPressed = false;
    }

    protected virtual void OnPressed() {
      wasPressed = true;
    }
  }
}