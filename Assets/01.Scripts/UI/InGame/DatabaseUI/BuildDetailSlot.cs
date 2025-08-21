using Project_Train.DataManage.CoreDataBaseSystem;
using UnityEngine;
namespace Project_Train.UIManage.InGameSceneUI
{

    public class BuildDetailSlot : DataDetailSlot
    {
        
        public override void SetData(DataDetailSO detail)
        {
            if (detail is BuildDetailSO buildDetail)
            {
                // TODO : After ResourceSystem

            }
            else
            {
                Debug.LogError($"Unexpected Detail data : detail is {detail.DetailType}");
            }
        }
    }
}