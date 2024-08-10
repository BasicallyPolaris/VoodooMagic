using UnityEngine;

namespace VoodooMagic.Scripts.UI
{

  public class UIInteractorController : MonoBehaviour
  {

    [Header("Hand - UI Interactor")]
    [SerializeField] private UIInteractor leftHand;
    [SerializeField] private UIInteractor rightHand;

    [Header("Controller - UI Interactor")]
    [SerializeField] private UIInteractor leftController;
    [SerializeField] private UIInteractor rightController;

    public void DisableUIInteractor()
    {
      leftHand.SetIsActive(false);
      rightHand.SetIsActive(false);
      leftController.SetIsActive(false);
      rightController.SetIsActive(false);
    }

    public void EnableUIInteractor()
    {
      leftHand.SetIsActive(true);
      rightHand.SetIsActive(true);
      leftController.SetIsActive(true);
      rightController.SetIsActive(true);
    }

    public void DisableHandUIInteractor()
    {
      leftHand.SetIsActive(false);
      rightHand.SetIsActive(false);
    }

    public void EnableHandUIInteractor()
    {
      leftHand.SetIsActive(true);
      rightHand.SetIsActive(true);
    }

    public void DisableControllerUIInteractor()
    {
      leftController.SetIsActive(false);
      rightController.SetIsActive(false);
    }

    public void EnableControllerUIInteractor()
    {
      leftController.SetIsActive(true);
      rightController.SetIsActive(true);
    }
  }
}