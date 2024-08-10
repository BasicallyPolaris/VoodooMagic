using UnityEngine;
using VoodooMagic.Scripts.Constants;

namespace VoodooMagic.Scripts.Raycast
{
  public class RaycastAPI : MonoBehaviour
  {
    [Header("Root Game Object")]
    [SerializeField] private Transform raycastSource;
    [SerializeField] private RaycastDirection raycastDirection;

    public Transform RaycastSource { get => raycastSource; private set => raycastSource = value; }
    public RaycastDirection RayDirection { get => raycastDirection; private set => raycastDirection = value; }
  }
}