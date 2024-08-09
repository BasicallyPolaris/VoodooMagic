using UnityEngine;
using UnityEngine.Events;
using VoodooMagic.Scripts.Controlls.API;

namespace VoodooMagic.Scripts.Controlls
{
  public class ControllManager : MonoBehaviour
  {
    [Header("Hand Tracking")]
    [SerializeField] private HandTrackingAPI handTrackingAPILeft;
    [SerializeField] private HandTrackingAPI handTrackingAPIRight;

    [Header("Controller API")]
    [SerializeField] private ControllerAPI controllerAPILeft;
    [SerializeField] private ControllerAPI controllerAPIRight;

    private void OnEnable()
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
    }

    public void EnableHandTracking()
    {
      handTrackingAPILeft.gameObject.SetActive(true);
      handTrackingAPIRight.gameObject.SetActive(true);
    }

    public void EnableController()
    {
      controllerAPILeft.gameObject.SetActive(true);
      controllerAPIRight.gameObject.SetActive(true);
    }

    public void DisableHandTracking()
    {
      handTrackingAPILeft.gameObject.SetActive(false);
      handTrackingAPIRight.gameObject.SetActive(false);
    }

    public void DisableController()
    {
      controllerAPILeft.gameObject.SetActive(false);
      controllerAPIRight.gameObject.SetActive(false);
    }

    public void EnableAll()
    {
      handTrackingAPILeft.gameObject.SetActive(true);
      handTrackingAPIRight.gameObject.SetActive(true);

      controllerAPILeft.gameObject.SetActive(true);
      controllerAPIRight.gameObject.SetActive(true);
    }

    public void DisableAll()
    {
      handTrackingAPILeft.gameObject.SetActive(false);
      handTrackingAPIRight.gameObject.SetActive(false);

      controllerAPILeft.gameObject.SetActive(false);
      controllerAPIRight.gameObject.SetActive(false);
    }
  }
}