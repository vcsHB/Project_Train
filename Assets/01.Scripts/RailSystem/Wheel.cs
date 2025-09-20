using Project_Train.RailSystem;
using UnityEngine;

namespace Project_Train
{
    public class Wheel : MonoBehaviour
    {
        [HideInInspector] public float speed;
        public Rail CurrentRail { get; private set; }
        private float _progress = 0f;
        private bool _isReversedRail = false;

        private bool _isInitialized = false;

        public void Initialize(Rail startRail, Transform carTransform)
        {
            CurrentRail = startRail;

			InitializeProgress(startRail, carTransform);

            _isInitialized = true;
		}

        private void InitializeProgress(Rail rail, Transform carTransform)
        {
            // 레일의 로컬 좌표계 기준으로 휠의 위치를 변환
            Vector3 wheelPosInRailSpace = transform.position - rail.transform.position - RailMath.railOffset;

            RailMath.GetPoints(rail.type, out Vector3 p0, out Vector3 p1, out Vector3 p2);

            // t 값을 찾기 위해 월드 포지션을 사용하지 않고, 레일 기준의 상대 위치를 사용해야 함
            _progress = RailMath.GetTForQuadraticBezier(p0, p1, p2, wheelPosInRailSpace);

            // t 값이 0~1 범위를 벗어난 경우, 시작점과 끝점 중 더 가까운 쪽으로 progress와 방향을 설정
            if (_progress < 0f || _progress > 1f)
            {
                float distToStart = Vector3.Distance(wheelPosInRailSpace, p0);
                float distToEnd = Vector3.Distance(wheelPosInRailSpace, p2);

                if (distToStart < distToEnd)
                {
                    _progress = 0f;
                    _isReversedRail = false;
                }
                else
                {
                    _progress = 1f;
                    _isReversedRail = true;
                }
            }
            else
            {
                Vector3 tangent = RailMath.GetQuadraticBezierTangent(p0, p1, p2, _progress);
                float dot = Vector3.Dot(tangent, carTransform.forward);
                _isReversedRail = dot < 0;
            }
        }


        void Update()
        {
            if (Mathf.Approximately(speed, 0f) || !_isInitialized || CurrentRail == null) return;

            // 1. 진행도 업데이트
            // 각 레일의 실제 길이는 다를 수 있으므로, 근사치로 RailLength를 사용하거나,
            // 더 정확하게는 각 커브의 길이를 계산해야 함. 지금은 상수를 사용.
            float railLengthApproximation = RailMath.RailLength;
            float step = (speed / railLengthApproximation) * Time.deltaTime;
            _progress += _isReversedRail ? -step : step;

            // 2. 레일 전환 처리
            if (_progress >= 1f && !_isReversedRail)
            {
                _progress = 1f;
                SetPositionAndRotation();
                TransitionToNextRail();
            }
            else if (_progress <= 0f && _isReversedRail)
            {
                _progress = 0f;
                SetPositionAndRotation();
                TransitionToNextRail();
            }
            else
            {
                SetPositionAndRotation();
            }
        }

        private void SetPositionAndRotation()
        {
            if (CurrentRail == null) return;

            transform.position = CurrentRail.GetPositionByProgress(_progress);

            RailMath.GetPoints(CurrentRail.type, out Vector3 p0, out Vector3 p1, out Vector3 p2);
            
            // GetQuadraticBezierTangent는 로컬 방향을 반환하므로 레일의 회전을 적용해줘야 함.
            // 하지만 현재 Rail.cs는 회전을 사용하지 않고 position만 사용하므로, 월드 좌표계에서 다시 계산.
            p0 += CurrentRail.transform.position;
            p1 += CurrentRail.transform.position;
            p2 += CurrentRail.transform.position;

            Vector3 direction = RailMath.GetQuadraticBezierTangent(p0, p1, p2, _progress);

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
            if (CurrentRail == null) return;

            Vector3 exitPoint = _isReversedRail ? CurrentRail.StartPos : CurrentRail.EndPos;

            Rail nextRail = RailManager.Instance.GetRail(exitPoint, CurrentRail);

            CurrentRail = nextRail;

            if (nextRail == null)
            {
                Debug.LogWarning($"End of the line at {exitPoint}. Stopping wheel.", this);
                speed = 0; // 레일이 없으면 정지
                return;
            }

            // 새 레일에서의 진행 방향과 시작 progress 설정
            if (Vector3.Distance(nextRail.StartPos, exitPoint) < 0.1f)
            {
                _isReversedRail = false; // 정방향
                _progress = 0f;
            }
            else if (Vector3.Distance(nextRail.EndPos, exitPoint) < 0.1f)
            {
                _isReversedRail = true; // 역방향
                _progress = 1f;
            }
            else
            {
                Debug.LogError($"Could not find a connecting rail at {exitPoint}. Stopping wheel.", this);
                speed = 0; // 연결되는 레일을 찾을 수 없으면 정지
                CurrentRail = null;
            }
        }
    }
}