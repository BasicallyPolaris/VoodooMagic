using UnityEngine;
using VoodooMagic.Scripts.Controlls.API;
using VoodooMagic.Scripts.SoftPatch;
using VoodooMagic.Scripts.Constants;
using VoodooMagic.Scripts.UI;
using System.Collections;

namespace VoodooMagic.Scripts.Controlls
{
  /// <summary>
  /// Manages the control behavior of the game.
  /// </summary>
  public class ControllManager : MonoBehaviour
  {
    [Header("Handtracking API")]
    /// <summary>
    /// The left hand tracking API.
    /// </summary>
    [SerializeField] private HandTrackingAPI handTrackingAPILeft;

    /// <summary>
    /// The right hand tracking API.
    /// </summary>
    [SerializeField] private HandTrackingAPI handTrackingAPIRight;

    [Header("Controller API")]
    /// <summary>
    /// The left controller API.
    /// </summary>
    [SerializeField] private ControllerAPI controllerAPILeft;

    /// <summary>
    /// The right controller API.
    /// </summary>
    [SerializeField] private ControllerAPI controllerAPIRight;

    [Header("SoftPatch Controller")]
    [SerializeField] private SoftPatchController softPatchController;

    /// <summary>
    /// The current controller state.
    /// </summary>
    [Header("Controller State")]
    [SerializeField] private ControllerState controllerState;


    /// <summary>
    /// Called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
      if (handTrackingAPILeft == null || handTrackingAPIRight == null)
      {
        Debug.LogError("Hand Tracking API is not set.");
        return;
      }

      if (controllerAPILeft == null || controllerAPIRight == null)
      {
        Debug.LogError("Controller API is not set.");
        return;
      }

      if (controllerState == ControllerState.SoftPatchInteraction)
      {
        softPatchController.EnableSPS();
      }
    }

    /// <summary>
    /// Called every frame.
    /// </summary>
    public void OnUpdate(Transform raycastSource, RaycastDirection raycastDirection, UIInteractor uiInteractor)
    {
      switch (controllerState)
      {
        case ControllerState.SoftPatchInteraction:
          break;
        case ControllerState.UIInteraction:
          uiInteractor.OnUpdate(raycastSource, raycastDirection);
          break;
        case ControllerState.Idle:
          break;
        default:
          break;
      }
    }

    /// <summary>
    /// Handles the pinch gesture event.
    /// </summary>
    /// <param name="raycastSource">The root transform used for raycasting.</param>
    /// <param name="rayDirection">The direction of the raycast.</param>
    /// <param name="spawner">The SoftPatchSpawner component used for spawning soft patches.</param>
    public void OnPinch(SoftPatchSpawner spawner, UIInteractor uiInteractor)
    {
      switch (controllerState)
      {
        case ControllerState.SoftPatchInteraction:
          spawner.SpawnSoftPatch();
          break;
        case ControllerState.UIInteraction:
          uiInteractor.OnPinch();
          break;
        case ControllerState.Idle:
          break;
        default:
          break;
      }
    }

    /// <summary>
    /// Handles the grab gesture event.
    /// </summary>
    /// <param name="raycastSource">The root transform used for raycasting.</param>
    /// <param name="raycastDirection">The direction of the raycast.</param>
    /// <param name="spawner">The SoftPatchSpawner component used for spawning soft patches.</param>
    public void OnGrab(SoftPatchSpawner spawner, UIInteractor uiInteractor)
    {
      switch (controllerState)
      {
        case ControllerState.SoftPatchInteraction:
          spawner.DeleteHighlightedObject();
          break;
        case ControllerState.UIInteraction:
          uiInteractor.OnGrab();
          break;
        case ControllerState.Idle:
          break;
        default:
          break;
      }
    }

    public void SetControllerState(ControllerState state)
    {
      switch (state)
      {
        case ControllerState.SoftPatchInteraction:
          softPatchController.EnableSPS();
          break;
        case ControllerState.UIInteraction:
          softPatchController.DisableSPS();
          break;
        case ControllerState.Idle:
          softPatchController.DisableSPS();
          break;
        default:
          break;
      }
      controllerState = state;
    }

    /// <summary>
    /// Enables hand tracking.
    /// </summary>
    public void EnableHandTracking()
    {
      handTrackingAPILeft.gameObject.SetActive(true);
      handTrackingAPIRight.gameObject.SetActive(true);
    }

    /// <summary>
    /// Enables controller.
    /// </summary>
    public void EnableController()
    {
      controllerAPILeft.gameObject.SetActive(true);
      controllerAPIRight.gameObject.SetActive(true);
    }

    /// <summary>
    /// Disables hand tracking.
    /// </summary>
    public void DisableHandTracking()
    {
      handTrackingAPILeft.gameObject.SetActive(false);
      handTrackingAPIRight.gameObject.SetActive(false);
    }

    /// <summary>
    /// Disables controller.
    /// </summary>
    public void DisableController()
    {
      controllerAPILeft.gameObject.SetActive(false);
      controllerAPIRight.gameObject.SetActive(false);
    }

    /// <summary>
    /// Enables hand tracking and controller.
    /// </summary>
    public void EnableAll()
    {
      handTrackingAPILeft.gameObject.SetActive(true);
      handTrackingAPIRight.gameObject.SetActive(true);

      controllerAPILeft.gameObject.SetActive(true);
      controllerAPIRight.gameObject.SetActive(true);
    }

    /// <summary>
    /// Disables hand tracking and controller.
    /// </summary>
    public void DisableAll()
    {
      handTrackingAPILeft.gameObject.SetActive(false);
      handTrackingAPIRight.gameObject.SetActive(false);

      controllerAPILeft.gameObject.SetActive(false);
      controllerAPIRight.gameObject.SetActive(false);
    }
  }
}
