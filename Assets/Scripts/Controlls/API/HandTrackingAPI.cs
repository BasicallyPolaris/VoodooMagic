using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction.GrabAPI;
using Unity.VisualScripting;
using UnityEngine;
using VoodooMagic.Scripts.SoftPatch;
using VoodooMagic.Scripts.UI;

namespace VoodooMagic.Scripts.Controlls.API
{
    public class HandTrackingAPI : BaseControllAPI
    {
        [Header("Hand Tracking")]
        [SerializeField] private HandGrabAPI handGrabAPI;

        public ControllerState controllerState { private get; set; }
        private SoftPatchController softPatchController;
        private UIInteractorController uiInteractorController;

        protected override void Awake()
        {
            softPatchController = GameObject.FindWithTag("SoftPatchSystem").GetComponent<SoftPatchController>();
            uiInteractorController = GameObject.FindWithTag("UISystem").GetComponent<UIInteractorController>();
            base.Awake();
        }

        private void OnEnable()
        {
            if (handGrabAPI == null)
            {
                Debug.LogError("Hand Grab API is not set.");
                return;
            }

            switch (controllerState)
            {
                case ControllerState.UIInteraction:
                    uiInteractorController.DisableControllerUIInteractor();
                    break;
                case ControllerState.SoftPatchInteraction:
                    softPatchController.DisableControllerSPS();
                    break;
                default:
                    break;
            }
        }

        private void OnDisable()
        {
            switch (controllerState)
            {
                case ControllerState.UIInteraction:
                    uiInteractorController.EnableControllerUIInteractor();
                    break;
                case ControllerState.SoftPatchInteraction:
                    softPatchController.EnableControllerSPS();
                    break;
                default:
                    break;
            }
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
