using UnityEngine;
namespace Project_Train.DataManage.CoreDataBaseSystem
{

    public abstract class DataDetailSO : ScriptableObject
    {
        public abstract DataDetailType DetailType { get; }

    }
}