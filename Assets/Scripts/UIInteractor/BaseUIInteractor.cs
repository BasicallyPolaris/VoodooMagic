using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoodooMagic.Scripts.Constants;


namespace VoodooMagic.Scripts.UIInteractor
{
    public class BaseUIInteractor : MonoBehaviour
    {

        [Header("Raycast Settings")]
        [Tooltip("Consider taking the root game object of the component  the script is on.")]
        [SerializeField] protected Transform raycastSource;
        [SerializeField] protected RayDirection rayDirection = RayDirection.Forward;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}