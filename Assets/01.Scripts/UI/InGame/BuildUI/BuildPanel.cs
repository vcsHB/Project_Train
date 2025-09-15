using System;
using Project_Train.BuildSystem;
using Project_Train.TerrainSystem;
using UnityEngine;

namespace Project_Train.UIManage.InGameSceneUI.BuildUI
{
    public class BuildPanel : GameUIPanel
    {
        [SerializeField] private BuildingManager _buildManager;
        [SerializeField] private BuildController _buildController;
        [SerializeField] private BuildCategoryGroup _categoryGroup;
        [Header("Detail Information Panel Setting")]
        [SerializeField] private TerrainDataPanel _terrainDataPanel;
        private Vector3 _buildPosition;
        private BuildingDataSO _buildTarget;

        protected override void Awake()
        {
            base.Awake();
            _categoryGroup.OnBuildingSelectedEvent += HandleSelect;
            _buildController.OnPointSelectEvent += HandlePointSelect;

        }

        private void HandleSelect(BuildingDataSO data)
        {
            _buildTarget = data;
        }

        public override void Open()
        {
            base.Open();
            _buildTarget = null;
            SetBuildMode(true);
        }

        public override void Close()
        {
            base.Close();
            SetBuildMode(false);
        }

        public void SetBuildMode(bool value)
        {
            _buildController.SetBuildMode(value);
        }

        private void HandlePointSelect(BuildPoint point)
        {
            if (IsPanelEnabled)
                _terrainDataPanel.SetData(point.TerrainStatus);

            if (_buildTarget != null)
            {
                _buildManager.Build(_buildTarget, point);
            }
        }


    }
}
