using UnityEngine;

namespace Project_Train.RailSystem
{
	public class Cart : MonoBehaviour
	{
		public float speed = 5f;
		private Vector3Int currentRailPosition = default;
		private float _progress = 0f;
		private bool _isReversedRail = false;

		private void Start()
		{
			InitializeStartPosition();
		}

		private void InitializeStartPosition()
		{
			currentRailPosition = Vector3Int.FloorToInt(transform.position - RailVectors.railOffset);

			int railHalfLength = (int)RailVectors.RailLength / 2;

			if (RailManager.Instance.GetRailType(currentRailPosition) == ERailType.None)
			{
				for (int x = -railHalfLength; x < railHalfLength; x += railHalfLength)
				{
					for (int y = -railHalfLength; y < railHalfLength; y += railHalfLength)
					{
						for (int z = -railHalfLength; z < railHalfLength; z += railHalfLength)
						{
							var newSelectedRailPos = currentRailPosition + new Vector3Int(x, y, z);
							var newSelectedRailType = RailManager.Instance.GetRailType(newSelectedRailPos);
							if (newSelectedRailType != ERailType.None)
							{
								InitializeProcess(newSelectedRailPos, newSelectedRailType);
								return;
							}
						}
					}
				}
			}
			else
			{
				var newSelectedRailType = RailManager.Instance.GetRailType(currentRailPosition);
				InitializeProcess(currentRailPosition, newSelectedRailType);
			}
		}

		private void InitializeProcess(Vector3Int newSelectedRailPos, ERailType newSelectedRailType)
		{
			var t = (currentRailPosition - newSelectedRailPos);
			RailVectors.GetPoints(newSelectedRailType, out Vector3 p0, out Vector3 p1, out Vector3 p2);

			_progress = GetTForQuadraticBezier(p0, p1, p2, t);

			if (_progress < 0.0f)
			{
				if (Vector3.Distance(currentRailPosition, newSelectedRailPos) < 0.1f)
				{
					_isReversedRail = false; // 정방향 연결
					_progress = 0f;
				}
				else if (Vector3.Distance(currentRailPosition, newSelectedRailPos) < 0.1f)
				{
					_isReversedRail = true; // 역방향 연결
					_progress = 1f;
				}
			}

			Debug.Log(_progress);

			currentRailPosition = newSelectedRailPos;
		}

		void Update()
		{
			if (speed == 0) return;

			ERailType currentRailType = RailManager.Instance.GetRailType(currentRailPosition);
			if (currentRailType == ERailType.None)
			{
				speed = 0;
				Debug.LogWarning($"No rail found at {currentRailPosition}. Stopping cart.");
				return;
			}

			// 1. 진행도 업데이트
			float railLength = RailVectors.RailLength;
			float step = (speed / railLength) * Time.deltaTime;
			_progress += _isReversedRail ? -step : step;

			// 2. 레일 전환 처리
			if (_progress >= 1f && !_isReversedRail)
			{
				_progress = 1f;
				SetPositionAndRotation(currentRailType, _progress);
				TransitionToNextRail();
			}
			else if (_progress <= 0f && _isReversedRail)
			{
				_progress = 0f;
				SetPositionAndRotation(currentRailType, _progress);
				TransitionToNextRail();
			}
			else
			{
				SetPositionAndRotation(currentRailType, _progress);
			}
		}

		private void SetPositionAndRotation(ERailType railType, float t)
		{
			Vector3 p0, p1, p2;
			RailVectors.GetPoints(railType, out p0, out p1, out p2);

			p0 += currentRailPosition;
			p1 += currentRailPosition;
			p2 += currentRailPosition;

			transform.position = GetQuadraticBezierPoint(p0, p1, p2, t) + RailVectors.railOffset;

			Vector3 direction = GetQuadraticBezierTangent(p0, p1, p2, t);

			if (direction != Vector3.zero)
			{
				if (_isReversedRail)
				{
					direction *= -1;
				}
				transform.rotation = Quaternion.LookRotation(direction);
			}
		}

		private void TransitionToNextRail()
		{
			var previousRailPosition = currentRailPosition;
			var previousRailType = RailManager.Instance.GetRailType(previousRailPosition);

			Vector3 p0, p1, p2;
			RailVectors.GetPoints(previousRailType, out p0, out p1, out p2);
			p0 += previousRailPosition;
			p1 += previousRailPosition;
			p2 += previousRailPosition;

			Vector3 previousRailEndPoint;
			Vector3 exitDirection;

			if (!_isReversedRail) // 정방향으로 진행 중이었을 경우 (t=1에서 탈출)
			{
				previousRailEndPoint = p2;
				exitDirection = (p2 - p1).normalized; // t=1에서의 접선 방향
			}
			else // 역방향으로 진행 중이었을 경우 (t=0에서 탈출)
			{
				previousRailEndPoint = p0;
				exitDirection = (p0 - p1).normalized; // t=0에서의 접선 방향
			}

			currentRailPosition += Vector3Int.RoundToInt(exitDirection * RailVectors.RailLength);

			var currentRailType = RailManager.Instance.GetRailType(currentRailPosition);
			if (currentRailType == ERailType.None)
			{
				Debug.LogWarning($"End of the line at {currentRailPosition}. Stopping cart.");
				speed = 0; // 선로가 없으면 정지
				return;
			}

			Vector3 newP0, newP1, newP2;
			RailVectors.GetPoints(currentRailType, out newP0, out newP1, out newP2);
			newP0 += currentRailPosition;
			newP2 += currentRailPosition;

			if (Vector3.Distance(newP0, previousRailEndPoint) < 0.1f)
			{
				_isReversedRail = false; // 정방향 연결
				_progress = 0f;
			}
			else if (Vector3.Distance(newP2, previousRailEndPoint) < 0.1f)
			{
				_isReversedRail = true; // 역방향 연결
				_progress = 1f;
			}
			else
			{
				Debug.LogError($"Could not find a connecting rail at {currentRailPosition} coming from {previousRailPosition}. Stopping cart.");
				speed = 0; // 연결되는 레일이 없으면 정지
			}
		}

		public static float GetTForQuadraticBezier(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p)
		{
			// 베지어 곡선 공식을 t에 대한 벡터 방정식 At^2 + Bt + C = 0 형태로 변환합니다.
			Vector3 A = p0 - 2 * p1 + p2;
			Vector3 B = 2 * (p1 - p0);
			Vector3 C = p0 - p;

			// A의 크기가 매우 작으면 곡선이 아닌 직선에 가깝습니다.
			if (A.sqrMagnitude < 0.0001f)
			{
				if (B.sqrMagnitude < 0.0001f) return 0.0f;
				float t = -Vector3.Dot(B, C) / B.sqrMagnitude;
				return t;
			}

			// 벡터 방정식의 양변에 A를 내적하여 스칼라 2차 방정식으로 변환합니다.
			float a = Vector3.Dot(A, A);
			float b = Vector3.Dot(A, B);
			float c = Vector3.Dot(A, C);

			float discriminant = b * b - 4 * a * c;
			if (discriminant < 0)
			{
				return -1.0f;
			}

			float sqrtDiscriminant = Mathf.Sqrt(discriminant);
			float t1 = (-b + sqrtDiscriminant) / (2 * a);
			float t2 = (-b - sqrtDiscriminant) / (2 * a);

			if (t1 >= 0 && t1 <= 1) return t1;
			if (t2 >= 0 && t2 <= 1) return t2;

			return -1.0f;
		}

		Vector3 GetQuadraticBezierPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
		{
			t = Mathf.Clamp01(t);
			float oneMinusT = 1f - t;
			return oneMinusT * oneMinusT * p0 +
				   2f * oneMinusT * t * p1 +
				   t * t * p2;
		}

		Vector3 GetQuadraticBezierTangent(Vector3 p0, Vector3 p1, Vector3 p2, float t)
		{
			return (2f * (1f - t) * (p1 - p0) + 2f * t * (p2 - p1)).normalized;
		}
	}
}