using Project_Train.BuildSystem;
namespace Project_Train.DataManage.CoreDataBaseSystem
{

    public class BuildDetailSO : DataDetailSO
    {
        public override DataDetailType DetailType => DataDetailType.Build;
        // RequireResource : TODO => REsourceSystem

        public Building buildingPrefab;
        

    }
}