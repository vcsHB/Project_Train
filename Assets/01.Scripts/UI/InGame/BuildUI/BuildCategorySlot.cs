using System;
using Project_Train.BuildSystem;
using Project_Train.UIManage.Decoration;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Project_Train.UIManage.InGameSceneUI.BuildUI
{

    public class BuildCategorySlot : HoverWidthHighLighter
    {
        public event Action<BuildingDataSO> OnBuildingSelectedEvent;
        [Header("Category Essential Setting")]
        [SerializeField] private TextMeshProUGUI _categoryNameText;
        [SerializeField] private Image _categoryIconImage;
        [SerializeField] private float _buildSlotWidthSize = 150f;
        [SerializeField] private float _offset;

        [SerializeField] private Transform _contentTrm;
        [Header("Category Setting")]
        [SerializeField] private string _categoryName = "CATEGORY";
        [SerializeField] private Sprite _categorySprite;
        private BuildSlot[] _buildSlots;

        protected override void Awake()
        {
            base.Awake();
            if (_contentTrm == null)
            {
                Debug.LogError("[!] ContentTrm is null! Can't Initialize BuildSlots");
                return;
            }
            _buildSlots = _contentTrm.GetComponentsInChildren<BuildSlot>();

            for (int i = 0; i < _buildSlots.Length; i++)
            {
                _buildSlots[i].OnSlotSelectedEvent += HandleSelectBuildTarget;
            }
        }

        private void HandleSelectBuildTarget(BuildingDataSO data)
        {

            OnBuildingSelectedEvent?.Invoke(data);
        }

        public void SetSelectStatus(BuildingDataSO data)
        {
            for (int i = 0; i < _buildSlots.Length; i++)
            {
                _buildSlots[i].SetSelectMark(_buildSlots[i].BuildingData == data);
            }
        }


#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_categoryNameText)
                _categoryNameText.text = _categoryName;

            if (_categoryIconImage)
                _categoryIconImage.sprite = _categorySprite;
            if (_contentTrm == null)
            {
                Debug.LogError("[!] ContentTrm is null! Can't Initialize totalEnableWidth");
                return;
            }
            int contentAmount = _contentTrm.childCount;
            float totalEnableWidth = _defaultWidth + (_buildSlotWidthSize * contentAmount) + _offset;

            _enableWidth = totalEnableWidth;
            Debug.Log($"Auto Value Set: TotalEnableWidth={totalEnableWidth}");


        }

#endif


    }
}