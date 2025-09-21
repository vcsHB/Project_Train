using System;
using Project_Train.Combat.CasterSystem.HitBody;
using UnityEngine;
using UnityEngine.UI;
namespace Project_Train.UIManage.InGameSceneUI
{

    public class HealthGauge : MonoBehaviour
    {
        [SerializeField] private Health _owner;
        [SerializeField] private Image _fillImage;

        private void Awake()
        {
            _owner.OnHealthDecreaseEvent += HandleHealthValueChanged;
            HandleHealthValueChanged(_owner.CurrentHealth, _owner.MaxHealth);
        }

        private void HandleHealthValueChanged(float current, float max)
        {
            float ratio = current / max;
            _fillImage.fillAmount = ratio;
        }
    }
}