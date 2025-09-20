using UnityEngine;

namespace  Project_Train.RailSystem
{
    public class Rail : MonoBehaviour
    {
        public ERailType type;

		public Vector3 StartPos { get; private set; }
		public Vector3 EndPos { get; private set; }

		private void Awake()
		{
			RailMath.GetPoints(type, out Vector3 p0, out Vector3 p1, out Vector3 p2);

			var trmPos = RailMath.GetRoundedPosition(transform.position);

			StartPos = p0 + trmPos;
			EndPos = p2 + trmPos;
			RailManager.Instance.AddRail(this, StartPos);
			RailManager.Instance.AddRail(this, EndPos);
		}

		public Vector3 GetPositionByProgress(float progress)
		{
			Vector3 p0, p1, p2;
			RailMath.GetPoints(type, out p0, out p1, out p2);

			p0 += transform.position;
			p1 += transform.position;
			p2 += transform.position;

			return RailMath.GetQuadraticBezierPoint(p0, p1, p2, progress) + RailMath.railOffset;
		}

#if UNITY_EDITOR
		private void OnDrawGizmos()
		{
			var position = transform.position;
			var offset = RailMath.railOffset;

			RailMath.GetPoints(type, out Vector3 p0, out Vector3 p1, out Vector3 p2);

			Gizmos.color = Color.blue;
			Gizmos.DrawLine(position + offset + p0,	position + offset + p1);
			Gizmos.color = Color.red;
			Gizmos.DrawLine(position + offset + p1, position + offset + p2);
			Gizmos.color = Color.white;
		}

		private void OnDrawGizmosSelected()
		{
			RailMath.GetPoints(type, out Vector3 p0, out Vector3 p1, out Vector3 p2);

			var trmPos = RailMath.GetRoundedPosition(transform.position);

			StartPos = p0 + trmPos;
			EndPos = p2 + trmPos;
			Debug.Log($"StartPos : {StartPos}");
			Debug.Log($"EndPos : {EndPos}");
		}
#endif
	}
}