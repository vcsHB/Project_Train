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
			ERailType currentRailType = RailManager.Instance.GetRailType(currentRailPos);

			// 1. 진행도 업데이트
			float railLength = GetRailLength(currentRailType); // 레일 타입별 길이 미리 계산
			progress += (speed / railLength) * Time.deltaTime;

			// 2. 위치와 방향 계산
			Vector3 worldPosition = default;
			Vector3 direction = default;
			CalculateNextPosition(currentRailType, progress);

			transform.position = worldPosition;
			transform.rotation = Quaternion.LookRotation(direction);

			// 3. 다음 레일로 이동
			if (progress >= 1f)
			{
				progress = 0f;
				currentRailPos = GetNextRailPos(currentRailPos, currentRailType);
			}
		}

		private Vector3Int GetNextRailPos(Vector3Int currentRailPos, ERailType currentRailType)
		{
			return default;
		}

		private float GetRailLength(ERailType currentRailType)
		{
			return default;
		}

		Vector3 CalculateNextPosition(ERailType railType, float t)
		{
			Vector3 p0 = Vector3.zero, p1 = Vector3.zero, p2 = Vector3.zero;

			switch (railType)
			{
				case ERailType.Straight_NS:
					p0 = new Vector3(0, 0, 0.5f);
					p1 = Vector3.zero;
					p2 = new Vector3(0, 0, -0.5f);
					break;
				case ERailType.Straight_EW:
					p0 = new Vector3(0.5f, 0, 0);
					p1 = Vector3.zero;
					p2 = new Vector3(-0.5f, 0, 0);
					break;

				case ERailType.Curve_NE:
					p0 = new Vector3(0, 0, 0.5f);
					p1 = Vector3.zero;
					p2 = new Vector3(0.5f, 0, 0);
					break;
				case ERailType.Curve_ES:
					p0 = new Vector3(0.5f, 0, 0);
					p1 = Vector3.zero;
					p2 = new Vector3(0, 0, -0.5f);
					break;
				case ERailType.Curve_SW:
					p0 = new Vector3(0, 0, -0.5f);
					p1 = Vector3.zero;
					p2 = new Vector3(-0.5f, 0, 0);
					break;
				case ERailType.Curve_WN:
					p0 = new Vector3(-0.5f, 0, 0);
					p1 = Vector3.zero;
					p2 = new Vector3(0, 0, 0.5f);
					break;

				case ERailType.Ascending_N:
					p0 = new Vector3(0, 0, -0.5f);
					p1 = new Vector3(0, 0.25f, 0);
					p1 = new Vector3(0, 0.5f, 0.5f);
					break;
				case ERailType.Ascending_E:
					p0 = new Vector3(-0.5f, 0, 0);
					p1 = new Vector3(0, 0.25f, 0);
					p1 = new Vector3(0.5f, 0.5f, 0);
					break;
				case ERailType.Ascending_S:
					p0 = new Vector3(0, 0, 0.5f);
					p1 = new Vector3(0, 0.25f, 0);
					p1 = new Vector3(0, 0.5f, -0.5f);
					break;
				case ERailType.Ascending_W:
					p0 = new Vector3(0.5f, 0, 0);
					p1 = new Vector3(0, 0.25f, 0);
					p1 = new Vector3(-0.5f, 0.5f, 0);
					break;
			}

			return GetQuadraticBezierPoint(p0, p1, p2, t);
		}

		Vector3 GetQuadraticBezierPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
		{
			return (p0 * (1 - t) + p1 * t) * (1 - t) 
				 + (p1 * (1 - t) + p2 * t) * t;
		}
	}
}
