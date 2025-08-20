using System;
using System.Collections.Generic;
using Project_Train.DataManage.CoreDataBaseSystem;
using UnityEngine;
namespace Project_Train.UIManage.InGameSceneUI
{

    public class DatabasePanel : MonoBehaviour
    {
        [SerializeField] private DataGroupSO _dataGroup;
        [SerializeField] private Transform _contentTrm;

        [SerializeField] private DatabaseSlot _slotPrefab;
        private List<DatabaseSlot> _slotList = new();

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            for (int i = 0; i < _dataGroup.datas.Length; i++)
            {
                DatabaseSlot newSlot = Instantiate(_slotPrefab, _contentTrm);
                newSlot.SetData(_dataGroup.datas[i], true); // TODO //  DataLoad
                _slotList.Add(newSlot);
                newSlot.OnSlotSelectEvent += HandleSelect;
            }
        }

        private void HandleSelect(DataSO SO)
        {
        }
    }
}