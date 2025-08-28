using System;
using Project_Train.Core.Input;
using Unity.Cinemachine;
using UnityEngine;

namespace Project_Train.ViewControl
{
    public class ViewController : MonoBehaviour
    {
        [Header("Essential Setting")]
        [SerializeField] private CinemachineCamera _camera;
        [SerializeField] private Transform _lookPinPosition;

        [Header("Rotation Setting")]
        [SerializeField] private float _clampVerticalMaxAngle = 80f;
        [SerializeField] private float _clampVerticalMinAngle = 80f;
        [SerializeField] private float _sensitivity = 2f; // 회전 민감도

        [Header("Zoom Setting")]
        [SerializeField] private float _distance = 5f; // 기본 카메라 거리
        [SerializeField] private float _minDistance = 2f; // 최소 줌
        [SerializeField] private float _maxDistance = 15f; // 최대 줌
        [SerializeField] private float _zoomSpeed = 2f; // 줌 속도

        private readonly string _inputHoldKey = "OnMouseMoveClickEvent";
        private readonly string _inputReleaseKey = "OnMouseMoveReleaseEvent";
        private readonly string _inputZoomScollKey = "OnMouseScrollEvent";

        private bool _isHolding;
        private float _yaw;   // 좌우 회전값
        private float _pitch; // 상하 회전값

        private void Awake()
        {
            InputReader.AddListener(_inputReleaseKey, HandleViewRelease);
            InputReader.AddListener(_inputHoldKey, HandleViewHold);
            InputReader.AddListener<float>(_inputZoomScollKey, HandleScroll);
        }

        private void HandleScroll(float delta)
        {
            _distance -= delta * _zoomSpeed;
            _distance = Mathf.Clamp(_distance, _minDistance, _maxDistance);

            UpdateCameraPosition();
        }

        private void Update()
        {
            if (_isHolding)
            {
                float deltaX = Input.GetAxis("Mouse X");
                float deltaY = Input.GetAxis("Mouse Y");

                _yaw += deltaX * _sensitivity;
                _pitch -= deltaY * _sensitivity;

                _pitch = Mathf.Clamp(_pitch, _clampVerticalMinAngle, _clampVerticalMaxAngle);

                UpdateCameraPosition();
            }
        }

        private void UpdateCameraPosition()
        {
            if (_lookPinPosition == null || _camera == null) return;

            // 회전값으로 쿼터니언 생성
            Quaternion rotation = Quaternion.Euler(_pitch, _yaw, 0f);

            // 중심 기준 카메라 위치 계산
            Vector3 targetPos = _lookPinPosition.position - (rotation * Vector3.forward * _distance);

            // 카메라 이동 & 중심 바라보기
            _camera.transform.position = targetPos;
            _camera.transform.LookAt(_lookPinPosition.position);
        }

        private void HandleViewHold() => _isHolding = true;
        private void HandleViewRelease() => _isHolding = false;
    }
}
