using UnityEngine;

namespace Project_Train.RailSystem
{
	public class Cart : MonoBehaviour
	{
		public float speed = 5f;
		private Vector3Int currentRailPos;
		private float progress = 0f;

		void Update()
		{
			ERailType currentRailType = RailManager.Instance.railGrid[currentRailPos];

			// 1. 진행도 업데이트
			// 레일 길이에 따라 속도를 조절해야 하지만, 일단은 간단하게!
			float railLength = GetRailLength(currentRailType); // 레일 타입별 길이 미리 계산
			progress += (speed / railLength) * Time.deltaTime;

			// 2. 위치와 방향 계산
			Vector3 worldPosition;
			Vector3 direction;
			CalculatePositionAndDirection(currentRailType, progress, out worldPosition, out direction);

			transform.position = worldPosition;
			transform.rotation = Quaternion.LookRotation(direction);

			// 3. 다음 레일로 이동
			if (progress >= 1f)
			{
				progress = 0f;
				currentRailPos = GetNextRailPos(currentRailPos, currentRailType);
			}
		}

		// 이게 바로 실시간 방향을 구하는 핵심 함수!
		void CalculatePositionAndDirection(ERailType type, float t, out Vector3 pos, out Vector3 dir)
		{
			Vector3 p0, p1, p2, p3; // 레일 경로를 정의하는 제어점들

			// 현재 레일 타입(type)에 따라 제어점(p0, p1, p2, p3)을 설정
			// 예를 들어 Curve_NE 라면,
			// p0 = 남쪽 끝, p1,p2 = 곡선을 만드는 제어점, p3 = 동쪽 끝

			// 베지어 곡선 공식으로 위치(pos)와 접선(dir) 계산
			// 2차 베지어 (제어점 3개: P0, P1, P2)
			// pos = (1-t)²P0 + 2(1-t)tP1 + t²P2
			// dir = 2(1-t)(P1-P0) + 2t(P2-P1) (이걸 정규화하면 방향 벡터!)

			// 직선이라면 Lerp로 간단히
			// pos = Vector3.Lerp(startPoint, endPoint, t);
			// dir = (endPoint - startPoint).normalized;

			// 임시값 할당
			pos = Vector3.zero;
			dir = Vector3.forward;
		}
	}
}
