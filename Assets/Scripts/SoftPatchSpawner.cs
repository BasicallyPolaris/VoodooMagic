using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class SoftPatchSpawner : MonoBehaviour
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

    [Header("Actions")]
    [SerializeField] private InputActionReference spawnAction;
    [SerializeField] private InputActionReference deleteAction;

    [Header("Prefabs")]
    [SerializeField] private GameObject softPatchPrefab;

    [Header("Spawn Location")]
    [SerializeField] private Transform rootTransform;
    [SerializeField] private Transform spawnLocation;
    [SerializeField] private GameObject container;
    [SerializeField] private RayDirection rayDirection = RayDirection.Forward;


    [Header("Highlight")]
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private float rayLength = 100f;
    private Material originalMaterial;
    private GameObject highlightedObject;


    private bool isSpawnHeld = false;
    private bool isDeleteHeld = false;

    void Update()
    {
        HighlightCushionInRange();
    }

    private void OnEnable()
    {
        spawnAction.action.performed += OnSpawnPerformed;
        spawnAction.action.canceled += OnSpawnCanceled;
        spawnAction.action.Enable();

        deleteAction.action.performed += OnDeletePerformed;
        deleteAction.action.canceled += OnDeleteCanceled;
        deleteAction.action.Enable();
    }

    private void OnDisable()
    {
        spawnAction.action.performed -= OnSpawnPerformed;
        spawnAction.action.canceled -= OnSpawnCanceled;
        spawnAction.action.Disable();

        deleteAction.action.performed -= OnDeletePerformed;
        deleteAction.action.canceled -= OnDeleteCanceled;
        deleteAction.action.Disable();
    }

    private void OnSpawnPerformed(InputAction.CallbackContext context)
    {
        if (!isSpawnHeld)
        {
            GameObject softPatch = Instantiate(softPatchPrefab, spawnLocation.transform.position, spawnLocation.transform.rotation);
            softPatch.transform.SetParent(container.transform);

            isSpawnHeld = true;
        }
    }

    private void OnSpawnCanceled(InputAction.CallbackContext context)
    {
        isSpawnHeld = false;
    }

    private void OnDeletePerformed(InputAction.CallbackContext context)
    {
        if (!isDeleteHeld && highlightedObject != null)
        {
            Destroy(highlightedObject);
            highlightedObject = null;
            originalMaterial = null;
        }

        isDeleteHeld = true;
    }

    private void OnDeleteCanceled(InputAction.CallbackContext context)
    {
        isDeleteHeld = false;
    }

    private void HighlightCushionInRange()
    {
        RaycastHit hit;
        Vector3 rayOrigin = rootTransform.position - GetRayDirection() * 0.1f;
        Vector3 rayDirection = GetRayDirection();

        Debug.DrawRay(rayOrigin, rayDirection * rayLength, Color.red);

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

    private void RemoveHighlight()
    {
        if (highlightedObject != null)
        {
            highlightedObject.GetComponent<Renderer>().material = originalMaterial;
            highlightedObject = null;
        }
    }
    private Vector3 GetRayDirection()
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