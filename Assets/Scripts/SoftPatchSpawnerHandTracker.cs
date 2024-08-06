using System.ComponentModel;
using Oculus.Interaction.GrabAPI;
using UnityEngine;

public class SoftPatchSpawnerHandTracker : MonoBehaviour
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

    [Header("Hand Tracking")]
    [SerializeField] private HandGrabAPI handGrabAPI;

    [Header("Prefabs")]
    [SerializeField] private GameObject softPatchPrefab;

    [Header("Controller API GameObject")]
    [Description("This is to make sure only the Hand Tracking API GameObject is active or the Controllers")]
    [SerializeField] private GameObject controllerAPI;

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
    private bool wasPalmGrabbing = false;
    private bool wasPinchGrabbing = false;

    void Update()
    {
        HighlightCushionInRange();
        CheckGrabStates();
    }

    private void OnEnable()
    {
        if (handGrabAPI == null)
        {
            Debug.LogError("Hand Grab API is not set.");
            return;
        }

        controllerAPI.SetActive(false);
    }

    private void OnDisable()
    {
        RemoveHighlight();
        controllerAPI.SetActive(true);
    }

    private void CheckGrabStates()
    {
        bool isPalmGrabbing = handGrabAPI.IsHandPalmGrabbing(GrabbingRule.DefaultPalmRule);
        bool isPinchGrabbing = handGrabAPI.IsHandPinchGrabbing(GrabbingRule.DefaultPinchRule);

        // Check palm grab state changes
        if (isPalmGrabbing && !wasPalmGrabbing)
        {
            OnPalmGrabStarted();
        }
        else if (!isPalmGrabbing && wasPalmGrabbing)
        {
            OnPalmGrabEnded();
        }

        // Check pinch grab state changes
        if (isPinchGrabbing && !wasPinchGrabbing)
        {
            OnPinchGrabStarted();
        }
        else if (!isPinchGrabbing && wasPinchGrabbing)
        {
            OnPinchGrabEnded();
        }

        wasPalmGrabbing = isPalmGrabbing;
        wasPinchGrabbing = isPinchGrabbing;
    }

    private void OnPalmGrabStarted()
    {
        OnDeletePerformed();
    }

    private void OnPalmGrabEnded()
    {
        OnDeleteCanceled();
    }

    private void OnPinchGrabStarted()
    {
        OnSpawnPerformed();
    }

    private void OnPinchGrabEnded()
    {
        OnSpawnCanceled();
    }

    private void OnSpawnPerformed()
    {
        if (!isSpawnHeld && !wasPalmGrabbing)
        {
            GameObject softPatch = Instantiate(softPatchPrefab, spawnLocation.transform.position, spawnLocation.transform.rotation);
            softPatch.transform.SetParent(container.transform);
            isSpawnHeld = true;
        }
    }

    private void OnSpawnCanceled()
    {
        isSpawnHeld = false;
    }

    private void OnDeletePerformed()
    {
        if (!isDeleteHeld && highlightedObject != null)
        {
            Destroy(highlightedObject);
            highlightedObject = null;
            originalMaterial = null;
        }
        isDeleteHeld = true;
    }

    private void OnDeleteCanceled()
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