using Project_Train.DataManage.CoreDataBaseSystem;
using UnityEngine;
namespace Project_Train.BuildSystem
{
    [CreateAssetMenu(menuName = "SO/Database/BuildingData")]
    public class BuildingDataSO : DataSO
    {
        public Building buildingPrefab;

#if UNITY_EDITOR

        private void OnValidate()
        {
            if (buildingPrefab == null)
            {
                Debug.LogWarning($"[BuildingDataSO Warning] buildingPrefab is Null. (name:{dataName})");
                return;
            }

            buildingPrefab.SetBuildingData(this);
        }
#endif
    }
}