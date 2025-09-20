using TMPro;
using Unity.IntegerTime;
using UnityEngine;
using UnityEngine.UI;
namespace Project_Train.UIManage.InGameSceneUI
{

    public class WaveTunnelInfoPanel : MonoBehaviour
    {

        [Header("Essential Settings")]
        [SerializeField] private TextMeshProUGUI _currentLeftEnemyText;

        [SerializeField] private TextMeshProUGUI _leftTimeToNextWave;
        [SerializeField] private Image _leftTimeGaugeImage;

        public void RefreshLeftEnemy(int amount)
        {
            _currentLeftEnemyText.text = amount.ToString();
        }

        public void RefreshLeftTime(float current, float max)
        {
            float ratio = current / max;
            _leftTimeGaugeImage.fillAmount = ratio;
            _leftTimeToNextWave.text = $"{Mathf.RoundToInt(current).ToString()}s";
        }
    }
}