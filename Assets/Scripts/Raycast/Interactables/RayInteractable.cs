
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


    public void OnPinch()
    {
      foreach (UnityEvent action in pinchActions)
      {
        action.Invoke();
      }
    }

    public void OnGrab()
    {
      foreach (UnityEvent action in grabActions)
      {
        action.Invoke();
      }
    }
    public abstract void highlight();

    public abstract void removeHighlight();
  }
}