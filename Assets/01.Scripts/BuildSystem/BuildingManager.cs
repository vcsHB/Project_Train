using System.Collections.Generic;
using Project_Train.DataManage.CoreDataBaseSystem;
using UnityEngine;
namespace Project_Train.BuildSystem
{

    public class BuildingManager : MonoBehaviour
    {
        [SerializeField] private List<Building> buildingList;

        private void Awake()
        {

        }

        public void Build(BuildingDataSO buildingData, Vector3 position)
        {
            BuildDetailSO detail = buildingData.GetDetail<BuildDetailSO>(DataDetailType.Build);
            

        }
    }
}