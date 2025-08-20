using UnityEngine;
namespace Project_Train.DataManage.CoreDataBaseSystem
{

    public class FunctionDetailSO : DataDetailSO
    {
        public override DataDetailType DetailType => DataDetailType.Function;
        public string description;
        public int level;
    }
}