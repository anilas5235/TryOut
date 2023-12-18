using UnityEngine;

namespace Project.Scripts.Utilities
{
    public static class VectorUtilities
    {
        public static Vector3 RotateAroundZ(Vector3 start, float angle)
        {
            return Quaternion.AngleAxis(angle, Vector3.forward) * start;
        }
    }
}