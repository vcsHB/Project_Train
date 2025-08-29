using System;
using Project_Train.Core.Input;
using Project_Train.TerrainSystem;
using UnityEditor.Timeline;
using UnityEngine;
namespace Project_Train.BuildSystem
{

    public class BuildController : MonoBehaviour
    {
        public event Action<BuildPoint> OnSelectEvent;
        [SerializeField] private BuildSelector _buildSelector;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private LayerMask _buildPointLayer;
        [SerializeField] private LayerMask _structureLayer;
        private readonly string _mouseEventKey = "OnMousePositionEvent";
        private readonly string _selectEventKey = "OnSelectEvent";
        private float _detectMaxDistance = 100f;
        private Vector2 _mousePosition;
        [SerializeField] private bool _isBuildMode;

        private void Awake()
        {
            InputReader.AddListener<Vector2>(_mouseEventKey, HandleMousePositionSet);
            InputReader.AddListener(_selectEventKey, HandleSelect);
        }
        public void SetBuildMode(bool value)
        {
            _isBuildMode = value;
        }

        private void HandleMousePositionSet(Vector2 mousePos)
        {
            if (!_isBuildMode) return;
            _mousePosition = mousePos;
        }

        private void HandleSelect()
        {
            if (!_isBuildMode) return;
            Ray ray = Camera.main.ScreenPointToRay(_mousePosition);
            bool isDetected = Physics.Raycast(ray, out RaycastHit hitInfo, _detectMaxDistance, _buildPointLayer);
            if (isDetected)
            {
                if (hitInfo.collider.TryGetComponent(out BuildPoint buildPoint))
                {
                    _buildSelector.SetPosition(buildPoint.PointPosition);
                    OnSelectEvent?.Invoke(buildPoint);
                }
            }

        }
    }
}