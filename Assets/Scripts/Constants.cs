using UnityEngine;

namespace VoodooMagic.Scripts.Constants
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

  public static class Util
  {
    public static Vector3 GetRayDirection(RayDirection rayDirection, Transform raycastSource)
    {
      switch (rayDirection)
      {
        case RayDirection.Forward:
          return raycastSource.forward;
        case RayDirection.Up:
          return raycastSource.up;
        case RayDirection.Down:
          return -raycastSource.up;
        case RayDirection.Left:
          return -raycastSource.right;
        case RayDirection.Right:
          return raycastSource.right;
        case RayDirection.Backward:
          return -raycastSource.forward;
        default:
          return raycastSource.forward;
      }
    }
  }
}