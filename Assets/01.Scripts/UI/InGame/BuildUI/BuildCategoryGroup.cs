using System;
using Project_Train.BuildSystem;
using UnityEngine;
namespace Project_Train.UIManage.InGameSceneUI.BuildUI
{

    public class BuildCategoryGroup : MonoBehaviour
    {
       
        public event Action<BuildingDataSO> OnBuildingSelectedEvent;
        [SerializeField] private BuildCategorySlot[] _categorys;
       

        private void Awake()
        {
            for (int i = 0; i < _categorys.Length; i++)
            {
                _categorys[i].OnBuildingSelectedEvent += HandleBuildTargetSelect;
            }
        }

        private void HandleBuildTargetSelect(BuildingDataSO data)
        {

            OnBuildingSelectedEvent?.Invoke(data);
            for (int i = 0; i < _categorys.Length; i++)
            {
                _categorys[i].SetSelectStatus(data);
            }
        }

    }
}