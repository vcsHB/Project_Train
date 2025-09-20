using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project_Train.UIManage.InGameSceneUI.EngineeringUI
{
    public class BuildingHealthGauge : MonoBehaviour
    {
        [SerializeField] private Image _gaugeImage;
        [SerializeField] private TextMeshProUGUI _valueText;
        [SerializeField] private float _fillDuration = 0.1f;


        public void ResetHealthGauge()
        {
            _gaugeImage.fillAmount = 0f;
        }
        /// <summary>
        /// Refresh Health Gauge for Value
        /// </summary>
        public void HandleGaugeRefresh(float current, float max)
        {
            //Debug.Log($"{current} / {max}");
            if (max <= 0f) return;

            float ratio = Mathf.Clamp01(current / max);
            _gaugeImage.DOFillAmount(ratio, _fillDuration);

            int curInt = Mathf.RoundToInt(current);
            int maxInt = Mathf.RoundToInt(max);

            _valueText.SetText("{0}/{1}", curInt, maxInt);
        }
    }
}
