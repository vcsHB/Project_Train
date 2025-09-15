using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Project_Train.BuildSystem.SubObjects
{

    public class BuildFrameProgressGauge : MonoBehaviour
    {
        [SerializeField] private BuildFrame _buildFrame;
        [SerializeField] private Image _gaugeFillImage;
        [SerializeField] private TextMeshProUGUI _progressText;


        private void Awake()
        {
            _buildFrame.OnBuildProgressChangeEvent += SetProgress;
        }

        private int _lastPercent;
        private int _lastSecondsLeft;

        private StringBuilder _stringBuilder = new StringBuilder(32);

        public void SetProgress(float current, float max)
        {
            if (max <= 0f)
            {
                Debug.LogError($"Max Value is Wrong Size [Value:{max}]");
                return;
            }

            float ratio = Mathf.Clamp01(current / max);
            int percent = (int)(ratio * 100f);
            int secondsLeft = (int)max - (int)current;

            if (percent == _lastPercent && secondsLeft == _lastSecondsLeft)
                return;

            _lastPercent = percent;
            _lastSecondsLeft = secondsLeft;

            _gaugeFillImage.fillAmount = ratio;

            _stringBuilder.Clear();
            _stringBuilder.Append(percent);
            _stringBuilder.Append("% (");
            _stringBuilder.Append(secondsLeft);
            _stringBuilder.Append("s)");

            _progressText.text = _stringBuilder.ToString();
        }
    }
}