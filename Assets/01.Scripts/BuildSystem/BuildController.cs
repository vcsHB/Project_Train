using System;
using Project_Train.Core.Input;
using Project_Train.DataManage.CoreDataBaseSystem;
using Project_Train.TerrainSystem;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Project_Train.BuildSystem
{

    public class BuildController : MonoBehaviour
    {
        public event Action<Building> OnBuildingSelectEvent;
        public event Action<BuildPoint> OnPointSelectEvent;
        [SerializeField] private BuildSelector _buildSelector;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private LayerMask _buildPointLayer;
        [SerializeField] private LayerMask _structureLayer;
        private readonly string _mouseEventKey = "OnMousePositionEvent";
        private readonly string _selectEventKey = "OnSelectEvent";
        private float _detectMaxDistance = 500000f;
        private Vector2 _mousePosition;
        [SerializeField] private bool _isBuildMode;
        [SerializeField] private bool _isSelectable = true;

        private void Awake()
        {
            InputReader.AddListener<Vector2>(_mouseEventKey, HandleMousePositionSet);
            InputReader.AddListener(_selectEventKey, HandleSelect);

        }

        private void OnDestroy()
        {
            //  !!!!  Essential Work
            BuildEventChannel.ClearBuildEvent();
        }


        public void SetSelectable(bool value)
        {
            _isSelectable = value;
        }

        public void SetBuildMode(bool value)
        {
            _isBuildMode = value;
        }

        private void HandleMousePositionSet(Vector2 mousePos)
        {
            _mousePosition = mousePos;
        }

        public void SetBuildTarget()
        {

        }

        private void HandleSelect()
        {
            if (EventSystem.current.IsPointerOverGameObject() || !_isSelectable) return;

            Ray ray = Camera.main.ScreenPointToRay(_mousePosition);
            if (_isBuildMode)
            {
                if (Physics.Raycast(ray, out var hitInfo, _detectMaxDistance, _buildPointLayer) &&
                    hitInfo.collider.TryGetComponent(out BuildPoint buildPoint))
                {
                    _buildSelector.SetPosition(buildPoint.PointPosition);
                    OnPointSelectEvent?.Invoke(buildPoint);
                }
            }
            else
            {
                if (Physics.Raycast(ray, out var buildingHitInfo, _detectMaxDistance, _structureLayer) &&
                    buildingHitInfo.collider.TryGetComponent(out Building building))
                {
                    FunctionDetailSO function = building.BuildingData.GetDetail<FunctionDetailSO>(DataDetailType.Function);
                    if (function != null)
                    {
                        _buildSelector.SetTowerRangeVisual(function.range, function.ignoreRatio);
                    }
                    else
                    {
                        _buildSelector.SetTowerRangeVisual(0f);
                    }
                    _buildSelector.SetPosition(building.transform.position);
                    OnBuildingSelectEvent?.Invoke(building);
                }
            }
        }



    }
}