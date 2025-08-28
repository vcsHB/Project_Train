using System;
using Project_Train.BuildSystem;
using Project_Train.TerrainSystem;
using UnityEngine;

namespace Project_Train.UIManage.InGameSceneUI.BuildUI
{
    public class BuildPanel : MonoBehaviour, IWindowPanel
    {
        [SerializeField] private BuildController _buildController;
        [Header("Detail Information Panel Setting")]
        [SerializeField] private TerrainDataPanel _terrainDataPanel; 

        private void Awake()
        {
            _buildController.OnSelectEvent += HandlePointSelect;
        }

        private void HandlePointSelect(BuildPoint point)
        {
            // TODO
            _terrainDataPanel.SetData(point.TerrainStatus);
        }

        public void Close()
        {

        }

        public void Open()
        {
        }
    }
}
