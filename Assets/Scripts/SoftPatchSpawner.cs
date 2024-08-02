using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class SoftPatchSpawner : MonoBehaviour
{
    [Header("Actions")]
    [SerializeField] private InputActionReference spawnAction;
    [SerializeField] private InputActionReference deleteAction;

    [Header("Prefabs")]
    [SerializeField] private GameObject softPatchPrefab;

    [Header("Spawn Location")]
    [SerializeField] private Transform spawnLocation;

    [Header("Highlight")]
    [SerializeField] private Material highlightMaterial;
    private Material originalMaterial;
    private GameObject highlightedObject;

    private bool isGripHeld = false;

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
        deleteAction.action.Enable();
    }

    private void OnDisable()
    {
        spawnAction.action.performed -= OnSpawnPerformed;
        spawnAction.action.canceled -= OnSpawnCanceled;
        spawnAction.action.Disable();

        deleteAction.action.performed -= OnDeletePerformed;
        deleteAction.action.Disable();
    }

    private void OnSpawnPerformed(InputAction.CallbackContext context)
    {
        if (!isGripHeld)
        {
            Instantiate(softPatchPrefab, spawnLocation.transform.position, spawnLocation.transform.rotation);
            isGripHeld = true;
        }
    }

    private void OnSpawnCanceled(InputAction.CallbackContext context)
    {
        isGripHeld = false;
    }

    private void OnDeletePerformed(InputAction.CallbackContext context)
    {
        RaycastHit hit;
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = transform.forward;
        float rayLength = 1000f;

        if (Physics.Raycast(rayOrigin, rayDirection, out hit, rayLength))
        {
            if (hit.collider.gameObject.CompareTag("SoftPatch"))
            {
                Destroy(hit.collider.gameObject);
            }
        }
    }

    private void HighlightCushionInRange()
    {
        RaycastHit hit;
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = transform.forward;
        float rayLength = 1000f;

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
}