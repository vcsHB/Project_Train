using Project_Train.DataManage.CoreDataBaseSystem;
using TMPro;
using UnityEngine;
namespace Project_Train.UIManage.InGameSceneUI
{

    public class SubjectDetailSlot : DataDetailSlot
    {
        [SerializeField] private TextMeshProUGUI _detailContentText;

        public override void SetData(DataDetailSO detail)
        {
            if (detail is SubjectDetailSO subjectData)
            {
                _detailContentText.text = subjectData.content;
            }
            else
            {
                Debug.LogError($"Unexpected Detail data : detail is {detail.DetailType}");
            }
        }
    }
}