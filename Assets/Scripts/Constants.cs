using UnityEngine;

namespace VoodooMagic.Scripts.Constants
{
  public enum RaycastDirection
  {
    Forward,
    Up,
    Down,
    Left,
    Right,
    Backward
  }

  public static class Util
  {
    public static Vector3 GetRayDirection(Transform raycastSource, RaycastDirection raycastDirection)
    {
      switch (raycastDirection)
      {
        case RaycastDirection.Forward:
          return raycastSource.forward;
        case RaycastDirection.Up:
          return raycastSource.up;
        case RaycastDirection.Down:
          return -raycastSource.up;
        case RaycastDirection.Left:
          return -raycastSource.right;
        case RaycastDirection.Right:
          return raycastSource.right;
        case RaycastDirection.Backward:
          return -raycastSource.forward;
        default:
          return raycastSource.forward;
      }
    }
  }
}