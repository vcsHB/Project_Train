using UnityEngine;
namespace Project_Train.DataManage.CoreDataBaseSystem
{

    public class SubjectDetailSO : DataDetailSO
    {
        public override DataDetailType DetailType => DataDetailType.Subject;
        [TextArea] public string content;

    }
}