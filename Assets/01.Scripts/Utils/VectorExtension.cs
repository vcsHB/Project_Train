using UnityEngine;

namespace Project_Train.Utils
{
	public static class VectorExtension
	{
		public static float InverseLerp(Vector3 a, Vector3 b, Vector3 value)
        {
			Vector3 ab = b - a;
			Vector3 av = value - a;

            if (ab.sqrMagnitude == 0)
            {
                return 0.0f;
            }

            float t = Vector3.Dot(av, ab) / ab.sqrMagnitude;
            return Mathf.Clamp01(t);
        }
    }
}
