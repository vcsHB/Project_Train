using Project_Train.RailSystem;
using System;
using UnityEditor;
using UnityEngine;

namespace Project_Train.Combat.TrainSystem
{
	[System.Serializable]
	public struct WheelDataInfo
	{
		public Transform wheelVisual;
		public Vector3 position;
		public Vector3Int currentRailPosition;
		private Vector3 _direction;
		public float progress;
		public bool isReversedRail;

		public Vector3 Direction
		{
			get { return _direction; }
			set 
			{
				wheelVisual.rotation = Quaternion.LookRotation(value);
				_direction = value; 
			}
		}
	}
	
	public class Train : MonoBehaviour
    {
		public float speed = 5f;
		[SerializeField] private WheelDataInfo _wheelA;
		[SerializeField] private WheelDataInfo _wheelB;

		[SerializeField] private Vector3[] _positions;

		private bool _isWent = false;

		public void Go(Rail startRail)
		{
			InitializeStartPosition(_wheelA);
			InitializeStartPosition(_wheelB);
			_isWent = true;
		}

		private void LateUpdate()
		{
			if (false == _isWent) return;
			VisualMove();
		}

		private void VisualMove()
		{
			var posA = _wheelA.position;
			var posB = _wheelB.position;

			var curPos = Vector3.Lerp(posA, posB, 0.5f) - (RailMath.railOffset * 0.5f);
			var curDir = (Vector3.Cross(posA, posB).sqrMagnitude > 0 ? (posA - posB) : (posB - posA)).normalized;

			transform.rotation = Quaternion.LookRotation(curDir);
			transform.position = curPos;
		}

		private void InitializeStartPosition(WheelDataInfo wheelDataInfo)
		{
			wheelDataInfo.position = Vector3Int.FloorToInt(transform.position - RailMath.railOffset);

			int railHalfLength = (int)RailMath.RailLength / 2;

			if (RailManager.Instance.GetRailType(wheelDataInfo.currentRailPosition) == ERailType.None)
			{
				for (int x = -railHalfLength; x < railHalfLength; x += railHalfLength)
				{
					for (int y = -railHalfLength; y < railHalfLength; y += railHalfLength)
					{
						for (int z = -railHalfLength; z < railHalfLength; z += railHalfLength)
						{
							var newSelectedRailPos = wheelDataInfo.currentRailPosition + new Vector3Int(x, y, z);
							var newSelectedRailType = RailManager.Instance.GetRailType(newSelectedRailPos);
							if (newSelectedRailType != ERailType.None)
							{
								InitializeProcess(wheelDataInfo, newSelectedRailPos, newSelectedRailType);
								return;
							}
						}
					}
				}
			}
			else
			{
				var newSelectedRailType = RailManager.Instance.GetRailType(wheelDataInfo.currentRailPosition);
				InitializeProcess(wheelDataInfo, wheelDataInfo.currentRailPosition, newSelectedRailType);
			}
		}

		private void InitializeProcess(WheelDataInfo wheelDataInfo, Vector3Int newSelectedRailPos, ERailType newSelectedRailType)
		{
			var t = (wheelDataInfo.currentRailPosition - newSelectedRailPos);
			RailMath.GetPoints(newSelectedRailType, out Vector3 p0, out Vector3 p1, out Vector3 p2);

			wheelDataInfo.progress = RailMath.GetTForQuadraticBezier(p0, p1, p2, t);

			if (wheelDataInfo.progress < 0.0f)
			{
				if (Vector3.Distance(wheelDataInfo.currentRailPosition, newSelectedRailPos) < 0.1f)
				{
					wheelDataInfo.isReversedRail = false; // 정방향 연결
					wheelDataInfo.progress = 0f;
				}
				else if (Vector3.Distance(wheelDataInfo.currentRailPosition, newSelectedRailPos) < 0.1f)
				{
					wheelDataInfo.isReversedRail = true; // 역방향 연결
					wheelDataInfo.progress = 1f;
				}
			}

			wheelDataInfo.currentRailPosition = newSelectedRailPos;
		}

		void Update()
		{
			MoveRail(_wheelA);
			MoveRail(_wheelB);
		}

		private void MoveRail(WheelDataInfo wheelDataInfo)
		{
			if (speed == 0) return;

			ERailType currentRailType = RailManager.Instance.GetRailType(wheelDataInfo.currentRailPosition);
			if (currentRailType == ERailType.None)
			{
				speed = 0;
				Debug.LogWarning($"No rail found at {wheelDataInfo.currentRailPosition}. Stopping cart.");
				return;
			}

			// 1. 진행도 업데이트
			float railLength = RailMath.RailLength;
			float step = (speed / railLength) * Time.deltaTime;
			wheelDataInfo.progress += wheelDataInfo.isReversedRail ? -step : step;

			// 2. 레일 전환 처리
			if (wheelDataInfo.progress >= 1f && false == wheelDataInfo.isReversedRail)
			{
				wheelDataInfo.progress = 1f;
				SetPositionAndRotation(wheelDataInfo, currentRailType);
				TransitionToNextRail(wheelDataInfo);
			}
			else if (wheelDataInfo.progress <= 0f && wheelDataInfo.isReversedRail)
			{
				wheelDataInfo.progress = 0f;
				SetPositionAndRotation(wheelDataInfo, currentRailType);
				TransitionToNextRail(wheelDataInfo);
			}
			else
			{
				SetPositionAndRotation(wheelDataInfo, currentRailType);
			}
		}

		private void TransitionToNextRail(WheelDataInfo wheelDataInfo)
		{
			var previousRailPosition = wheelDataInfo.currentRailPosition;
			var previousRailType = RailManager.Instance.GetRailType(previousRailPosition);

			Vector3 p0, p1, p2;
			RailMath.GetPoints(previousRailType, out p0, out p1, out p2);
			p0 += previousRailPosition;
			p1 += previousRailPosition;
			p2 += previousRailPosition;

			Vector3 previousRailEndPoint;
			Vector3 exitDirection;

			if (false == wheelDataInfo.isReversedRail) // 정방향으로 진행 중이었을 경우 (t=1에서 탈출)
			{
				previousRailEndPoint = p2;
				exitDirection = (p2 - p1).normalized; // t=1에서의 접선 방향
			}
			else // 역방향으로 진행 중이었을 경우 (t=0에서 탈출)
			{
				previousRailEndPoint = p0;
				exitDirection = (p0 - p1).normalized; // t=0에서의 접선 방향
			}

			exitDirection = RailMath.SnapToCardinal(exitDirection);
			wheelDataInfo.currentRailPosition += Vector3Int.RoundToInt(exitDirection * RailMath.RailLength);

			var currentRailType = RailManager.Instance.GetRailType(wheelDataInfo.currentRailPosition);
			if (currentRailType == ERailType.None)
			{
				Debug.LogWarning($"End of the line at {wheelDataInfo.currentRailPosition}. Stopping cart.");
				speed = 0; // 선로가 없으면 정지
				return;
			}

			Vector3 newP0, newP1, newP2;
			RailMath.GetPoints(currentRailType, out newP0, out newP1, out newP2);
			newP0 += wheelDataInfo.currentRailPosition;
			newP2 += wheelDataInfo.currentRailPosition;

			if (Vector3.Distance(newP0, previousRailEndPoint) < 0.1f)
			{
				wheelDataInfo.isReversedRail = false; // 정방향 연결
				wheelDataInfo.progress = 0f;
			}
			else if (Vector3.Distance(newP2, previousRailEndPoint) < 0.1f)
			{
				wheelDataInfo.isReversedRail = true; // 역방향 연결
				wheelDataInfo.progress = 1f;
			}
			else
			{
				Debug.LogError($"Could not find a connecting rail at {wheelDataInfo.currentRailPosition} coming from {previousRailPosition}. Stopping cart.");
				speed = 0; // 연결되는 레일이 없으면 정지
			}
		}

		public void SetPositionAndRotation(WheelDataInfo wheelDataInfo, ERailType railType)
		{
			Vector3 p0, p1, p2;
			RailMath.GetPoints(railType, out p0, out p1, out p2);

			p0 += wheelDataInfo.currentRailPosition;
			p1 += wheelDataInfo.currentRailPosition;
			p2 += wheelDataInfo.currentRailPosition;

			transform.position = RailMath.GetQuadraticBezierPoint(p0, p1, p2, wheelDataInfo.progress) + RailMath.railOffset;

			Vector3 direction = RailMath.GetQuadraticBezierTangent(p0, p1, p2, wheelDataInfo.progress);

			if (direction != Vector3.zero)
			{
				if (wheelDataInfo.isReversedRail)
				{
					direction *= -1;
				}
				transform.rotation = Quaternion.LookRotation(direction);
			}
		}
	}
}
