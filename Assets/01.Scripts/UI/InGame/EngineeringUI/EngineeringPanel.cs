using System;
using Project_Train.BuildSystem;
using UnityEngine;
namespace Project_Train.UIManage.InGameSceneUI.EngineeringUI
{

    public class EngineeringPanel : GameUIPanel
    {
        [SerializeField] private BuildController _buildController;
        [SerializeField] private StructureInformationPanel _infoPanel;

        protected override void Awake()
        {
            base.Awake();
            _buildController.OnBuildingSelectEvent += HandleBuildingSelect;
        }

        private void HandleBuildingSelect(Building building)
        {
            _infoPanel.SetStructureData(building);
            Open();
        }
    }
}