using UnityEngine;
using VoodooMagic.Scripts.Constants;

namespace VoodooMagic.Scripts.SoftPatch
{
  public class SoftPatchManager : MonoBehaviour
  {
    [Header("Prefabs")]
    [SerializeField] private GameObject softPatchPrefab;

    [Header("Spawn Location")]
    [Tooltip("Consider taking the root game object of the component  the script is on.")]
    [SerializeField] private GameObject container;

    [Header("Highlight")]
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private float rayLength = 100f;

    [Header("Hand Tracking - SoftPatchSpawner")]
    [SerializeField] private SoftPatchSpawner spsHandLeft;
    [SerializeField] private SoftPatchSpawner spsHandRight;

    [Header("Controller - SoftPatchSpawner")]
    [SerializeField] private SoftPatchSpawner spsControllerLeft;
    [SerializeField] private SoftPatchSpawner spsControllerRight;

    public GameObject getContainer()
    {
      return container;
    }

    public GameObject getSoftPatchPrefab()
    {
      return softPatchPrefab;
    }

    public Material getHighlightMaterial()
    {
      return highlightMaterial;
    }

    public float getRayLength()
    {
      return rayLength;
    }

    private void OnDisable()
    {
      DisableControllerSPS();
      DisableHandTrackingSPS();
    }

    public void EnableSPS()
    {
      EnableHandTrackingSPS();
      EnableControllerSPS();
    }

    public void DisableSPS()
    {
      DisableHandTrackingSPS();
      DisableControllerSPS();
    }

    public void EnableHandTrackingSPS()
    {
      spsHandLeft.enabled = true;
      spsHandRight.enabled = true;
    }

    public void EnableControllerSPS()
    {
      spsControllerLeft.enabled = true;
      spsControllerRight.enabled = true;
    }

    public void DisableHandTrackingSPS()
    {
      spsHandLeft.RemoveHighlight();
      spsHandRight.RemoveHighlight();
      spsHandLeft.enabled = false;
      spsHandRight.enabled = false;
    }

    public void DisableControllerSPS()
    {
      spsControllerLeft.RemoveHighlight();
      spsControllerRight.RemoveHighlight();
      spsControllerLeft.enabled = false;
      spsControllerRight.enabled = false;
    }
  }
}