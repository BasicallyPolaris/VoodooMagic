
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using VoodooMagic.Scripts.SoftPatch;

namespace VoodooMagic.Scripts.Controlls.API
{
  public abstract class BaseControllAPI : MonoBehaviour
  {
    [Header("SoftPatchSpawner")]
    [SerializeField] public SoftPatchSpawner softPatchSpawner;
    protected ControllManager controllManager;
    protected bool isPinching = false;
    protected bool isGrabbing = false;

    protected void Awake()
    {
      controllManager = GameObject.FindWithTag("ControllSystem").GetComponent<ControllManager>();
    }

    protected virtual void Update()
    {
      controllManager.OnUpdate();
    }

    protected void OnPinch()
    {
      controllManager.OnPinch(softPatchSpawner);
    }

    protected void OnGrab()
    {
      controllManager.OnGrab(softPatchSpawner);
    }
  }
}
