using System;
using Project_Train.DataManage.CoreDataBaseSystem;
using UnityEngine;
namespace Project_Train.UIManage.InGameSceneUI
{

    public class DatabasePanel : FadePanel
    {
        [SerializeField] private DatabaseListPanel _listPanel;
        [SerializeField] private DatabaseDetailPanel _detailPanel;


        protected override void Awake()
        {
            base.Awake();
            _listPanel.OnDataSelectedEvent += HandleRefreshDetail;
        }

        private void HandleRefreshDetail(DataSO data)
        {
            _detailPanel.SetData(data);
        }

        public override void Open()
        {
            base.Open();
        }
    }
}