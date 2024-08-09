
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace VoodooMagic.Scripts.Controlls
{
  public abstract class BaseControllAPI : MonoBehaviour
  {

    [Header("Actions")]
    [SerializeField] protected UnityEvent[] updateActions;
    [SerializeField] protected UnityEvent[] pinchActions;
    [SerializeField] protected UnityEvent[] grabActions;

    protected bool isPinching = false;
    protected bool isGrabbing = false;

    protected virtual void Update()
    {
      foreach (UnityEvent action in updateActions)
      {
        action.Invoke();
      }
    }

    protected void OnPinch()
    {
      foreach (UnityEvent action in pinchActions)
      {
        action.Invoke();
      }
    }

    protected void OnGrab()
    {
      foreach (UnityEvent action in grabActions)
      {
        action.Invoke();
      }
    }
  }
}
