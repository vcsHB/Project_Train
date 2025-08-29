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
        [SerializeField] private float _sensitivity = 2f; 

        [Header("Zoom Setting")]
        [SerializeField] private float _distance = 5f; 
        [SerializeField] private float _minZoomLevel = 2f; 
        [SerializeField] private float _maxZoomLevel = 15f;
        [SerializeField] private float _zoomSpeed = 2f; 

        private readonly string _inputHoldKey = "OnMouseMoveClickEvent";
        private readonly string _inputReleaseKey = "OnMouseMoveReleaseEvent";
        private readonly string _inputZoomScollKey = "OnZoomScrollEvent";

        private bool _isHolding;
        private float _yaw;  
        private float _pitch;

        private void Awake()
        {
            InputReader.AddListener(_inputReleaseKey, HandleViewRelease);
            InputReader.AddListener(_inputHoldKey, HandleViewHold);
            InputReader.AddListener<Vector2>(_inputZoomScollKey, HandleScroll);
        }

        private void HandleScroll(Vector2 delta)
        {
            float newZoomLevel = _camera.Lens.OrthographicSize - delta.y * _zoomSpeed;
            _camera.Lens.OrthographicSize = Mathf.Clamp(newZoomLevel, _minZoomLevel, _maxZoomLevel);
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

            Quaternion rotation = Quaternion.Euler(_pitch, _yaw, 0f);

            Vector3 targetPos = _lookPinPosition.position - (rotation * Vector3.forward * 15f);
            _camera.transform.position = targetPos;
            _camera.transform.LookAt(_lookPinPosition.position);
        }

        private void HandleViewHold() => _isHolding = true;
        private void HandleViewRelease() => _isHolding = false;
    }
}
