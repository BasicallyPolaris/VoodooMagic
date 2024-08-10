
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using VoodooMagic.Scripts.Constants;
using VoodooMagic.Scripts.SoftPatch;

namespace VoodooMagic.Scripts.Controlls.API
{
  public abstract class BaseControllAPI : MonoBehaviour
  {
    [Header("SoftPatchSpawner")]
    [SerializeField] public SoftPatchSpawner softPatchSpawner;
    protected ControllManager controllManager;
    protected Transform raycastSource;
    protected RayDirection rayDirection;
    protected bool isPinching = false;
    protected bool isGrabbing = false;

    protected void Awake()
    {
      controllManager = GameObject.FindWithTag("ControllSystem").GetComponent<ControllManager>();
      raycastSource = softPatchSpawner.RaycastSource;
      rayDirection = softPatchSpawner.RayDirection;
    }

    protected virtual void Update()
    {
      controllManager.OnUpdate();
    }

    protected void OnPinch()
    {
      controllManager.OnPinch(raycastSource, rayDirection, softPatchSpawner);
    }

    protected void OnGrab()
    {
      controllManager.OnGrab(raycastSource, rayDirection, softPatchSpawner);
    }
  }
}
