using UnityEngine;
using VoodooMagic.Scripts.Constants;

namespace VoodooMagic.Scripts.SoftPatch
{
  public class SoftPatchSpawner : MonoBehaviour
  {
    [Header("Controller / Hand Tracking")]
    [SerializeField] protected bool isHandTracking = false;

    [Header("Spawn Location")]
    [Tooltip("Consider taking the root game object of the component the script is on.")]
    [SerializeField] private Transform raycastSource;
    [SerializeField] protected Transform spawnLocation;
    [SerializeField] private RayDirection rayDirection = RayDirection.Forward;

    public Transform RaycastSource { get => raycastSource; private set => raycastSource = value; }
    public RayDirection RayDirection { get => rayDirection; private set => rayDirection = value; }

    private Material highlightMaterial;
    private float rayLength = 10f;
    private GameObject container;
    private GameObject softPatchPrefab;
    private Material originalMaterial;
    private GameObject highlightedObject;

    private void Awake()
    {
      container = GameObject.FindWithTag("SoftPatchSystem");
      softPatchPrefab = container.GetComponent<SoftPatchManager>().getSoftPatchPrefab();
      highlightMaterial = container.GetComponent<SoftPatchManager>().getHighlightMaterial();
      rayLength = container.GetComponent<SoftPatchManager>().getRayLength();
    }

    private void OnDisable()
    {
      RemoveHighlight();
    }

    private void Update()
    {
      HighlightCushionInRange();
    }

    public void SpawnSoftPatch()
    {
      GameObject softPatch = Instantiate(softPatchPrefab, spawnLocation.transform.position, spawnLocation.transform.rotation);
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
      Vector3 rayOrigin = raycastSource.position - Util.GetRayDirection(this.rayDirection, raycastSource) * 0.1f;
      Vector3 rayDirection = Util.GetRayDirection(this.rayDirection, raycastSource);

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