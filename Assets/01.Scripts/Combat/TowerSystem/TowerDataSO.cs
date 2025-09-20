using Project_Train.BuildSystem;
using Project_Train.DataManage.CoreDataBaseSystem;
using UnityEngine;
namespace Project_Train.Combat.TowerSystem
{
    [CreateAssetMenu(menuName = "SO/Database/TowerData")]
    public class TowerDataSO : BuildingDataSO
    {

#if UNITY_EDITOR

        private FunctionDetailSO _functionDetail;
        protected override void OnValidate()
        {
            base.OnValidate();

            if (_functionDetail == null)
                _functionDetail = GetDetail<FunctionDetailSO>(DataDetailType.Function);
            if (_functionDetail == null)
            {
                Debug.LogError($"Not Attached FunctionDetail Data (dataName:{dataName})");
                return;
            }
            if (buildingPrefab is Tower tower)
            {
                
                tower.SetDetectRange(_functionDetail.range, _functionDetail.ignoreRatio);
            }

        }

#endif
    }
}