using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction.GrabAPI;
using Unity.VisualScripting;
using UnityEngine;

namespace VoodooMagic.Scripts.Controlls.API
{
    public class HandTrackingAPI : BaseControllAPI
    {
        [Header("Hand Tracking")]
        [SerializeField] private HandGrabAPI handGrabAPI;

        [Header("Controller SPS GameObject")]
        [Tooltip("This is to make sure only the Hand Tracking API GameObject is active or the Controllers")]
        [SerializeField] private GameObject controllerSPSLeft;
        [SerializeField] private GameObject controllerSPSRight;

        private void OnEnable()
        {
            if (handGrabAPI == null)
            {
                Debug.LogError("Hand Grab API is not set.");
                return;
            }

            controllerSPSLeft.SetActive(false);
            controllerSPSRight.SetActive(false);
        }

        private void OnDisable()
        {
            controllerSPSLeft.SetActive(true);
            controllerSPSRight.SetActive(true);
        }

        protected override void Update()
        {
            base.Update();
            CheckHandState();
        }

        private void CheckHandState()
        {
            bool grabbing = handGrabAPI.IsHandPalmGrabbing(GrabbingRule.FullGrab);
            bool pinching = handGrabAPI.IsHandPinchGrabbing(GrabbingRule.DefaultPinchRule);

            if (grabbing && !isGrabbing)
            {
                OnGrab();
            }
            else if (!grabbing && isGrabbing)
            {
                isGrabbing = false;
            }

            if (pinching && !isPinching)
            {
                OnPinch();
            }
            else if (!pinching && isPinching)
            {
                isPinching = false;
            }

            isGrabbing = grabbing;
            isPinching = pinching;
        }
    }
}
