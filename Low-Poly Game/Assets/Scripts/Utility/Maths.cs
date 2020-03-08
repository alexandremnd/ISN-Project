using UnityEngine;

namespace Utility.Math
{
    public struct Vector
    {
        public static Vector2 AbsVector(Vector2 vector)
        {
            return new Vector2(Mathf.Abs(vector.x), Mathf.Abs(vector.y));
        }

        public static Vector3 AbsVector(Vector3 vector)
        {
            return new Vector3(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z));
        }
    }
}
