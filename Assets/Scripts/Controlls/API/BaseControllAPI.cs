
using UnityEngine;
using VoodooMagic.Scripts.Constants;
using VoodooMagic.Scripts.SoftPatch;
using VoodooMagic.Scripts.UI;

namespace VoodooMagic.Scripts.Controlls.API
{
  public abstract class BaseControllAPI : MonoBehaviour
  {
    [Header("SoftPatchSpawner")]
    [SerializeField] public SoftPatchSpawner softPatchSpawner;
    
    [Header("UIInteractor")]
    [SerializeField] public UIInteractor uiInteractor;

    protected ControllManager controllManager;
    protected Transform raycastSource;
    protected RaycastDirection raycastDirection;
    protected bool isPinching = false;
    protected bool isGrabbing = false;

    protected virtual void Awake()
    {
      controllManager = GameObject.FindWithTag("ControllSystem").GetComponent<ControllManager>();
      raycastSource = softPatchSpawner.RaycastSource;
      raycastDirection = softPatchSpawner.RayDirection;
    }

    protected virtual void Update()
    {
      controllManager.OnUpdate(raycastSource, raycastDirection, uiInteractor);
    }

    protected void OnPinch()
    {
      controllManager.OnPinch(softPatchSpawner, uiInteractor);
    }

    protected void OnGrab()
    {
      controllManager.OnGrab(softPatchSpawner, uiInteractor);
    }
  }
}
