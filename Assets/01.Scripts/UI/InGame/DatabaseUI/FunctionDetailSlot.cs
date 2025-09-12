using Project_Train.Core;
using Project_Train.DataManage.CoreDataBaseSystem;
using TMPro;
using UnityEngine;
namespace Project_Train.UIManage.InGameSceneUI.DataBaseUIManage
{

    public class FunctionDetailSlot : DataDetailSlot
    {
        [SerializeField] private TextMeshProUGUI _rangeText;
        [SerializeField] private TextMeshProUGUI _randomizeErrorAngleText;
        [SerializeField] private TextMeshProUGUI _fireCooltimeText;
        [SerializeField] private TextMeshProUGUI _attackAreaText;

        public override void SetData(DataDetailSO detail)
        {
            if (detail is FunctionDetailSO functionData)
            {
                _rangeText.text = functionData.range.ToString();
                _randomizeErrorAngleText.text = functionData.randomizeErrorAngle.ToString();
                _fireCooltimeText.text = $"{functionData.fireCooltime.ToString()}s";

                _attackAreaText.text = functionData.attackArea.ToDisplayString();

            }
            else
            {
                Debug.LogError($"Unexpected Detail data : detail is {detail.DetailType}");
            }
        }
    }
}
