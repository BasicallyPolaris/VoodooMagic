using UnityEngine;

namespace VoodooMagic.Scripts.SoftPatchSpawner
{
  public abstract class BaseSoftPatchSpawner : MonoBehaviour
  {
    public enum RayDirection
    {
      Forward,
      Up,
      Down,
      Left,
      Right,
      Backward
    }

    [Header("Prefabs")]
    [SerializeField] protected GameObject softPatchPrefab;

    [Header("Spawn Location")]
    [SerializeField] protected Transform rootTransform;
    [SerializeField] protected Transform spawnLocation;
    [SerializeField] protected GameObject container;
    [SerializeField] protected RayDirection rayDirection = RayDirection.Forward;

    [Header("Highlight")]
    [SerializeField] protected Material highlightMaterial;
    [SerializeField] protected float rayLength = 100f;
    protected Material originalMaterial;
    protected GameObject highlightedObject;

    protected bool isSpawnHeld = false;
    protected bool isDeleteHeld = false;

    protected virtual void Update()
    {
      HighlightCushionInRange();
    }

    protected virtual void OnEnable()
    {
      // Implement in derived classes
    }

    protected virtual void OnDisable()
    {
      RemoveHighlight();
    }

    protected abstract void CheckInputs();

    protected void SpawnSoftPatch()
    {
      if (!isSpawnHeld)
      {
        GameObject softPatch = Instantiate(softPatchPrefab, spawnLocation.transform.position, spawnLocation.transform.rotation);
        softPatch.transform.SetParent(container.transform);
        isSpawnHeld = true;
      }
    }

    protected void DeleteHighlightedObject()
    {
      if (!isDeleteHeld && highlightedObject != null)
      {
        Destroy(highlightedObject);
        highlightedObject = null;
        originalMaterial = null;
      }
      isDeleteHeld = true;
    }

    protected void HighlightCushionInRange()
    {
      RaycastHit hit;
      Vector3 rayOrigin = rootTransform.position - GetRayDirection() * 0.1f;
      Vector3 rayDirection = GetRayDirection();

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

    protected void RemoveHighlight()
    {
      if (highlightedObject != null)
      {
        highlightedObject.GetComponent<Renderer>().material = originalMaterial;
        highlightedObject = null;
      }
    }

    protected Vector3 GetRayDirection()
    {
      switch (rayDirection)
      {
        case RayDirection.Forward:
          return rootTransform.forward;
        case RayDirection.Up:
          return rootTransform.up;
        case RayDirection.Down:
          return -rootTransform.up;
        case RayDirection.Left:
          return -rootTransform.right;
        case RayDirection.Right:
          return rootTransform.right;
        case RayDirection.Backward:
          return -rootTransform.forward;
        default:
          return rootTransform.forward;
      }
    }
  }
}