using UnityEngine;
using VoodooMagic.Scripts.Constants;
using VoodooMagic.Scripts.RayInteraction;

namespace VoodooMagic.Scripts.SoftPatch
{
  public class SoftPatchSpawner : MonoBehaviour
  {
    [Header("Controller / Hand Tracking")]
    [SerializeField] protected bool isHandTracking = false;

    [Header("Spawn Location")]
    [Tooltip("Consider taking the root game object of the component the script is on.")]
    [SerializeField] private Transform raycastSource;
    [SerializeField] protected GameObject softpatchPreview;
    [SerializeField] private RaycastDirection raycastDirection = RaycastDirection.Forward;

    public Transform RaycastSource { get => raycastSource; private set => raycastSource = value; }
    public RaycastDirection RayDirection { get => raycastDirection; private set => raycastDirection = value; }

    private Material highlightMaterial;
    private float rayLength = 10f;
    private GameObject container;
    private GameObject softPatchPrefab;
    private Material originalMaterial;
    private GameObject highlightedObject;
    private bool isActive = false;

    private void Awake()
    {
      container = GameObject.FindWithTag("SoftPatchSystem");
      softPatchPrefab = container.GetComponent<SoftPatchController>().getSoftPatchPrefab();
      highlightMaterial = container.GetComponent<SoftPatchController>().getHighlightMaterial();
      rayLength = container.GetComponent<SoftPatchController>().getRayLength();
    }

    private void OnDisable()
    {
      RemoveHighlight();
    }

    private void Update()
    {
      if (isActive) HighlightCushionInRange();
    }

    public void SetIsActive(bool value)
    {
      if (value)
      {
        EnablePreview();
      }
      else
      {
        DisablePreview();
      }

      isActive = value;
    }

    public void SpawnSoftPatch()
    {
      if (highlightedObject != null)
      {
        return;
      }
      
      GameObject softPatch = Instantiate(softPatchPrefab, softpatchPreview.transform.position, softpatchPreview.transform.rotation);
      softPatch.transform.SetParent(container.transform);
    }

    public void DeleteHighlightedObject()
    {
      if (highlightedObject != null)
      {
        Destroy(highlightedObject);
        highlightedObject = null;
        originalMaterial = null;
      }
    }

    private void EnablePreview()
    {
      softpatchPreview.SetActive(true);
    }

    private void DisablePreview()
    {
      softpatchPreview.SetActive(false);
      RemoveHighlight();
    }

    private void RemoveHighlight()
    {
      if (highlightedObject != null && originalMaterial != null)
      {
        highlightedObject.GetComponent<Renderer>().material = originalMaterial;
        highlightedObject = null;
      }
    }

    private void HighlightCushionInRange()
    {
      RaycastHit hit;
      Vector3 rayOrigin = raycastSource.position - Util.GetRayDirection(raycastSource, raycastDirection) * 0.1f;
      Vector3 rayDirection = Util.GetRayDirection(raycastSource, raycastDirection);

      if (Physics.Raycast(rayOrigin, rayDirection, out hit, rayLength))
      {
        if (hit.collider.gameObject.CompareTag("SoftPatch"))
        {
          if (highlightedObject != hit.collider.gameObject)
          {
            RemoveHighlight();
            highlightedObject = hit.collider.gameObject;
            originalMaterial = highlightedObject.GetComponent<Renderer>().material;
            highlightedObject.GetComponent<Renderer>().material = highlightMaterial;
          }
          return;
        }
      }
      RemoveHighlight();
    }
  }
}