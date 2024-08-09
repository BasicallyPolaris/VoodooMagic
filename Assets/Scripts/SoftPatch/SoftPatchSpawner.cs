using UnityEngine;
using VoodooMagic.Scripts.Constants;

namespace VoodooMagic.Scripts.SoftPatch
{
  public class SoftPatchSpawner : MonoBehaviour
  {

    [Header("Spawn Location")]
    [Tooltip("Consider taking the root game object of the component  the script is on.")]
    [SerializeField] protected Transform raycastSource;
    [SerializeField] protected Transform spawnLocation;
    [SerializeField] protected RayDirection rayDirection = RayDirection.Forward;

    private Material highlightMaterial;
    private float rayLength = 100f;
    private GameObject container;
    private GameObject softPatchPrefab;
    private Material originalMaterial;
    private GameObject highlightedObject;

    private void Start()
    {
      container = GameObject.Find("SoftPatch-Logic");
      softPatchPrefab = container.GetComponent<SoftPatchManager>().getSoftPatchPrefab();
      highlightMaterial = container.GetComponent<SoftPatchManager>().getHighlightMaterial();
      rayLength = container.GetComponent<SoftPatchManager>().getRayLength();
    }

    private void OnDisable()
    {
      RemoveHighlight();
    }

    public void SpawnSoftPatch()
    {
      {
        GameObject softPatch = Instantiate(softPatchPrefab, spawnLocation.transform.position, spawnLocation.transform.rotation);
        softPatch.transform.SetParent(container.transform);
      }
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

    public void HighlightCushionInRange()
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

    public void RemoveHighlight()
    {
      if (highlightedObject != null)
      {
        highlightedObject.GetComponent<Renderer>().material = originalMaterial;
        highlightedObject = null;
      }
    }
  }
}