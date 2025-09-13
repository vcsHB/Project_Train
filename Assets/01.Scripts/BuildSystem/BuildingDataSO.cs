using Project_Train.DataManage.CoreDataBaseSystem;
using UnityEngine;
namespace Project_Train.BuildSystem
{
    [CreateAssetMenu(menuName = "SO/Database/BuildingData")]
    public class BuildingDataSO : DataSO
    {
        public Building buildingPrefab;
    }
}