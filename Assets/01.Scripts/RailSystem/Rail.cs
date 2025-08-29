using UnityEngine;

namespace  Project_Train.RailSystem
{
    public class Rail : MonoBehaviour
    {
        public ERailType type;

		private void Awake()
		{
            var position = GetRailPosition();
			RailManager.Instance.AddRail(type, position);
		}

        private Vector3Int GetRailPosition()
        {
            return Vector3Int.RoundToInt(transform.position);
        }

#if UNITY_EDITOR
		private void OnDrawGizmos()
		{
			var position = transform.position;
			var offset = RailVectors.railOffset;

			RailVectors.GetPoints(type, out Vector3 p0, out Vector3 p1, out Vector3 p2);

			Gizmos.color = Color.blue;
			Gizmos.DrawLine(position + offset + p0,	position + offset + p1);
			Gizmos.DrawLine(position + offset + p1, position + offset + p2);
			Gizmos.color = Color.white;
		}
#endif
	}
}
