using AYellowpaper.SerializedCollections;
using UnityEngine;
namespace Project_Train.DataManage.CoreDataBaseSystem
{

    public class BuildDetailSO : DataDetailSO
    {
        public override DataDetailType DetailType => DataDetailType.Build;
        public float buildDuration = 5f;
        // RequireResource : TODO => REsourceSystem
        [field:SerializeField] public SerializedDictionary<ResourceDataSO, int> RequireResource { get; private set; }






    }
}