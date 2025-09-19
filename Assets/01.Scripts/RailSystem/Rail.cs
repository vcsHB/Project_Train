using UnityEngine;

namespace  Project_Train.RailSystem
{
    public class Rail : MonoBehaviour
    {
        public ERailType type;
        public Transform startPoint;
        public Transform endPoint;

#if UNITY_EDITOR
		private void OnDrawGizmos()
		{
			// 기존 기즈모 코드는 그대로 유지 (시각화에 유용)
			var position = transform.position;
			var offset = RailMath.railOffset;

			RailMath.GetPoints(type, out Vector3 p0, out Vector3 p1, out Vector3 p2);

			Gizmos.color = Color.blue;
			Gizmos.DrawLine(position + offset + p0,	position + offset + p1);
			Gizmos.color = Color.red;
			Gizmos.DrawLine(position + offset + p1, position + offset + p2);
			Gizmos.color = Color.white;
		}
#endif
	}
}