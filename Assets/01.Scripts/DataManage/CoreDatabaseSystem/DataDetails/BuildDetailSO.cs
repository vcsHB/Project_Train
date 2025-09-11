using AYellowpaper.SerializedCollections;
using Project_Train.BuildSystem;
using Project_Train.ResourceSystem;
using UnityEngine;
namespace Project_Train.DataManage.CoreDataBaseSystem
{

    public class BuildDetailSO : DataDetailSO
    {
        public override DataDetailType DetailType => DataDetailType.Build;
        // RequireResource : TODO => REsourceSystem
        [field:SerializeField] public SerializedDictionary<ResourceDataSO, int> RequireResource { get; private set; }
        public Building buildingPrefab;






    }
}