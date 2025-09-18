using System.Collections.Generic;
using Project_Train.Core.Attribute;
using Project_Train.ViewControl;
using UnityEngine;

namespace Project_Train
{

    public class ViewAnchorController : MonoBehaviour
    {
        [SerializeField] private List<ViewAnchorPoint> _viewAnchorList;
        [SerializeField, ReadOnly] private int _currentViewAnchorIndex = 0;
        [SerializeField] private float _moveDuration;
        [SerializeField] private Transform _viewAnchorTrm;
        private float _currentTweenTime;
        private bool _isMoving;
        private Vector3 _previousPosition;
        private Vector3 _targetPosition;
        public Vector3 CurrentPointPosiotion => _viewAnchorList[_currentViewAnchorIndex].PointWorldPosition;

        private void Awake()
        {
            if (_viewAnchorTrm == null)
            {
                Debug.LogError("ViewAnchorTransform is Null. Can't Initialize view position");
                return;
            }
            _viewAnchorTrm.position = CurrentPointPosiotion;
        }

        public void AddNewViewAnchor(ViewAnchorPoint newPoint)
        {
            _viewAnchorList.Add(newPoint);
        }

        public void MovePoint(bool isForward)
        {
            if (_isMoving) return;

            _previousPosition = CurrentPointPosiotion;

            if (isForward)
                _currentViewAnchorIndex = _currentViewAnchorIndex <= 0 ? _viewAnchorList.Count - 1 : _currentViewAnchorIndex - 1;
            else
                _currentViewAnchorIndex = (_currentViewAnchorIndex + 1) % _viewAnchorList.Count;

            _targetPosition = CurrentPointPosiotion;
            _isMoving = true;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
                MovePoint(false);
            if (Input.GetKeyDown(KeyCode.D))
                MovePoint(true);

            if (_isMoving)
            {
                _currentTweenTime += Time.deltaTime;
                float ratio = _currentTweenTime / _moveDuration;
                _viewAnchorTrm.position = Vector3.Lerp(_previousPosition, _targetPosition, EaseOutCubic(ratio));
                if (_currentTweenTime > _moveDuration)
                {
                    _isMoving = false;
                    _currentTweenTime = 0f;
                    _viewAnchorTrm.position = _targetPosition;
                }

            }
        }

        private float EaseOutCubic(float x)
        {
            return 1 - Mathf.Pow(1f - x, 3);
        }
    }
}