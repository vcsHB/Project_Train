using System;
using Project_Train.BuildSystem;
using Project_Train.TerrainSystem;
using UnityEngine;

namespace Project_Train.UIManage.InGameSceneUI.BuildUI
{
    public class BuildPanel : GameUIPanel
    {
        [SerializeField] private BuildController _buildController;
        [Header("Detail Information Panel Setting")]
        [SerializeField] private TerrainDataPanel _terrainDataPanel;

        protected override void Awake()
        {
            base.Awake();
            _buildController.OnSelectEvent += HandlePointSelect;

        }

        public override void Open()
        {
            base.Open();
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
            // TODO
            if (IsPanelEnabled)
                _terrainDataPanel.SetData(point.TerrainStatus);
        }


    }
}
