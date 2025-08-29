using System;
using UnityEngine;

namespace Project_Train.RailSystem
{
	public class Cart : MonoBehaviour
	{
		public float speed = 5f;
		private Vector3Int currentRailPos = default;
		private float progress = 0f;

		void Update()
		{
			currentRailPos = Vector3Int.FloorToInt(transform.position - RailVectors.railOffset);

			ERailType currentRailType = RailManager.Instance.GetRailType(currentRailPos);

			// 1. 진행도 업데이트
			float railLength = GetRailLength(currentRailType); // 레일 타입별 길이 미리 계산

			Debug.Log(currentRailType);

			progress += (speed / railLength) * Time.deltaTime;

			// 2. 위치와 방향 계산
			Vector3 worldPosition = transform.position;
			Vector3 nextPosition = CalculateNextPosition(currentRailType, progress);
			Vector3 direction = (worldPosition - nextPosition).normalized;

			transform.position += direction * speed;
			//transform.rotation = Quaternion.LookRotation(direction);

			// 3. 다음 레일로 이동
			if (progress >= 1f)
			{
				progress = 0f;
				currentRailPos = GetNextRailPos(direction);
			} 
		}

		private Vector3Int GetNextRailPos(Vector3 moveDir)
		{
			return Vector3Int.FloorToInt(currentRailPos + moveDir);
		}

		private float GetRailLength(ERailType currentRailType)
		{
			return 1f;
		}

		Vector3 CalculateNextPosition(ERailType railType, float t)
		{
			Vector3 p0 = Vector3.zero, p1 = Vector3.zero, p2 = Vector3.zero;

			RailVectors.GetPoints(railType, out p0, out p1, out p2);
			p0 += currentRailPos;
			p1 += currentRailPos;
			p2 += currentRailPos;
			return GetQuadraticBezierPoint(p0, p1, p2, t);
		}

		Vector3 GetQuadraticBezierPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
		{
			return (p0 * (1 - t) + p1 * t) * (1 - t) 
				 + (p1 * (1 - t) + p2 * t) * t;
		}
	}
}
