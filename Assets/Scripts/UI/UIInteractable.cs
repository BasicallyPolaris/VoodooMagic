
using UnityEngine;

namespace VoodooMagic.Scripts.UI
{
  /// <summary>
  /// The UI Interactable interface.
  /// </summary>
  public abstract class UIInteractable : MonoBehaviour
  {
    public abstract void OnPinch();
    public abstract void OnGrab();
    public void highlight()
    {
      
    }

    public void removeHighlight()
    {

    }
  }
}