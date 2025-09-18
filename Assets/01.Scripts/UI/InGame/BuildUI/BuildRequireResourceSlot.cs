using Project_Train.Core.Attribute;
using Project_Train.DataManage.CoreDataBaseSystem;
using Project_Train.ResourceSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project_Train.UIManage.InGameSceneUI.BuildUI
{
    public class BuildRequireResourceSlot : MonoBehaviour
    {
        [Header("Slot Essential Settings")]
        [SerializeField] private Image _resourceIconImage;
        [SerializeField] private TextMeshProUGUI _resourceNameText;
        [SerializeField] private TextMeshProUGUI _resourceAmountText;
        [SerializeField] private ResourceManagerSO _resourceManager;

        [Header("Text Detail Settings")]
        [SerializeField] private Color _enoughTextColor = Color.white;
        [SerializeField] private Color _shortageTextColor = Color.red;

        [ReadOnly, SerializeField] private ResourceDataSO _resourceData;

        private int _requireAmount;
        private bool _isEventSubscribed;

        /// <summary>
        /// SetResource Require Data
        /// </summary>
        public void SetResourceData(ResourceDataSO resourceData, int amount)
        {
            _resourceData = resourceData;
            _requireAmount = amount;

            if (_resourceIconImage) _resourceIconImage.sprite = resourceData.iconSprite;
            if (_resourceNameText) _resourceNameText.text = resourceData.dataName;

            Subscribe();
            HandleRefreshResourceAmount(_resourceManager.GetResourceValue(resourceData.resourceType));
        }

        private void Subscribe()
        {
            if (_isEventSubscribed || _resourceData == null) return;

            _resourceManager.AddEventListener(_resourceData.resourceType, HandleRefreshResourceAmount);
            _isEventSubscribed = true;
        }

        /// <summary>
        /// Try Unsubscribe Event to resourceManager OnValueChangeEvent.
        /// </summary>
        public void Unsubscribe()
        {
            if (!_isEventSubscribed || _resourceData == null) return;

            _resourceManager.RemoveEventListener(_resourceData.resourceType, HandleRefreshResourceAmount);
            _isEventSubscribed = false;
        }

        private void HandleRefreshResourceAmount(int currentAmount)
        {
            if (_resourceAmountText == null) return;

            _resourceAmountText.color = currentAmount >= _requireAmount
                ? _enoughTextColor
                : _shortageTextColor;

            _resourceAmountText.text = $"{currentAmount}/{_requireAmount}";
        }

        public void SetEnable(bool value)
        {
            if (gameObject.activeSelf != value)
                gameObject.SetActive(value);
        }

        private void OnDisable()
        {
            // For Memory
            Unsubscribe();
        }
    }
}
