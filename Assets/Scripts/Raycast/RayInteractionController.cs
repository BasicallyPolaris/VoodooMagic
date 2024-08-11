using UnityEngine;

namespace VoodooMagic.Scripts.RayInteraction
{
  public class RayInteractionController : MonoBehaviour
  {

    [Header("Hand - Ray Interactor")]
    [SerializeField] private RayInteractor leftHand;
    [SerializeField] private RayInteractor rightHand;

    [Header("Controller -  Interactor")]
    [SerializeField] private RayInteractor leftController;
    [SerializeField] private RayInteractor rightController;

    public void DisableRayInteraction()
    {
      leftHand.SetIsActive(false);
      rightHand.SetIsActive(false);
      leftController.SetIsActive(false);
      rightController.SetIsActive(false);
    }

    public void EnableRayInteraction()
    {
      leftHand.SetIsActive(true);
      rightHand.SetIsActive(true);
      leftController.SetIsActive(true);
      rightController.SetIsActive(true);
    }

    public void DisableHandRayInteraction()
    {
      leftHand.SetIsActive(false);
      rightHand.SetIsActive(false);
    }

    public void EnableHandRayInteraction()
    {
      leftHand.SetIsActive(true);
      rightHand.SetIsActive(true);
    }

    public void DisableControllerRayInteraction()
    {
      leftController.SetIsActive(false);
      rightController.SetIsActive(false);
    }

    public void EnableControllerRayInteraction()
    {
      leftController.SetIsActive(true);
      rightController.SetIsActive(true);
    }
  }
}