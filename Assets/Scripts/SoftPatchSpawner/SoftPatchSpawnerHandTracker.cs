
using System.ComponentModel;
using Oculus.Interaction.GrabAPI;
using UnityEngine;


namespace VoodooMagic.Scripts.SoftPatchSpawner
{
    public class SoftPatchSpawnerHandTracker : BaseSoftPatchSpawner
    {
        [Header("Hand Tracking")]
        [SerializeField] private HandGrabAPI handGrabAPI;

        [Header("Controller API GameObject")]
        [Description("This is to make sure only the Hand Tracking API GameObject is active or the Controllers")]
        [SerializeField] private GameObject controllerAPI;

        private bool wasPalmGrabbing = false;
        private bool wasPinchGrabbing = false;

        protected override void Update()
        {
            base.Update();
            CheckInputs();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            if (handGrabAPI == null)
            {
                Debug.LogError("Hand Grab API is not set.");
                return;
            }

            controllerAPI.SetActive(false);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            controllerAPI.SetActive(true);
        }

        protected override void CheckInputs()
        {
            bool isPalmGrabbing = handGrabAPI.IsHandPalmGrabbing(GrabbingRule.DefaultPalmRule);
            bool isPinchGrabbing = handGrabAPI.IsHandPinchGrabbing(GrabbingRule.DefaultPinchRule);

            if (isPalmGrabbing && !wasPalmGrabbing)
            {
                DeleteHighlightedObject();
            }
            else if (!isPalmGrabbing && wasPalmGrabbing)
            {
                isDeleteHeld = false;
            }

            if (isPinchGrabbing && !wasPinchGrabbing)
            {
                SpawnSoftPatch();
            }
            else if (!isPinchGrabbing && wasPinchGrabbing)
            {
                isSpawnHeld = false;
            }

            wasPalmGrabbing = isPalmGrabbing;
            wasPinchGrabbing = isPinchGrabbing;
        }
    }
}