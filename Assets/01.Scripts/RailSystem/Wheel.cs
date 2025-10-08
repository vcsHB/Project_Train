using Project_Train.RailSystem;
using UnityEngine;

namespace Project_Train.RailSystem
{
    public class Wheel : MonoBehaviour
    {
        private ICar _ownerCar;
        
        public Rail CurrentRail { get; private set; }
        private float _progress = 0f;
        private bool _isReversedRail = false;
        private bool _isInitialized = false;
		private Vector3 _cachedPreiousDirection;

		public void Initialize(Rail startRail, ICar ownerCar)
        {
            this.CurrentRail = startRail;
            this._ownerCar = ownerCar;
            
			InitializeProgress(startRail, ownerCar);

            _isInitialized = true;
		}

        private void InitializeProgress(Rail rail, ICar ownerCar)
        {
            Vector3 wheelPosInRailSpace = transform.position - rail.transform.position - RailMath.railOffset;

            RailMath.GetPoints(rail.type, out Vector3 p0, out Vector3 p1, out Vector3 p2);

            _progress = RailMath.GetTForQuadraticBezier(p0, p1, p2, wheelPosInRailSpace);

            if (_progress is < 0f or > 1f)
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
                float dot = Vector3.Dot(tangent, ownerCar.transform.forward);
                _isReversedRail = dot < 0;
            }
        }

        void Update()
        {
            Move();
        }

        private void Move()
        {
            if (Mathf.Approximately(_ownerCar.CurrentSpeed, 0f) || !_isInitialized || CurrentRail == null) return;

            float railLengthApproximation = RailMath.RailLength;
            float step = (_ownerCar.CurrentSpeed / railLengthApproximation) * Time.deltaTime;
            _progress += _isReversedRail ? -step : step;

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
            if (CurrentRail)
            {
				transform.position = CurrentRail.GetPositionByProgress(_progress);

				RailMath.GetPoints(CurrentRail.type, out Vector3 p0, out Vector3 p1, out Vector3 p2);

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
            else
            {
				transform.position += _cachedPreiousDirection;
				transform.rotation = Quaternion.LookRotation(_cachedPreiousDirection);
			}
        }

        private void TransitionToNextRail()
        {
            if (null ==CurrentRail) return;

            Vector3 exitPoint = _isReversedRail ? CurrentRail.StartPos : CurrentRail.EndPos;

            Rail nextRail = RailManager.Instance.GetRail(exitPoint, CurrentRail);

            CurrentRail = nextRail;

            if (null == nextRail)
            {
                Debug.LogWarning($"End of the line at {exitPoint}. Stopping wheel.", this);
                return;
            }

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
                CurrentRail = null;
            }
        }
    }
}