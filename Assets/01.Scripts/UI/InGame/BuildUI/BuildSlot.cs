using System;
using Project_Train.BuildSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Project_Train.UIManage.InGameSceneUI.BuildUI
{

    public class BuildSlot : MonoBehaviour
    {
        public event Action<BuildingDataSO> OnSlotSelectedEvent;
        [SerializeField] private BuildingDataSO _buildingDataSO;
        public BuildingDataSO BuildingData => _buildingDataSO;
        [SerializeField] private Image _buildIconImage;
        [SerializeField] private TextMeshProUGUI _buildNameText;
        [SerializeField] private FadePanel _selectPanel;

        private Button _buttonCompo;

        private void Awake()
        {
            _buttonCompo = GetComponent<Button>();
            _buttonCompo.onClick.AddListener(HandleSlotSelected);

        }

        public void SetSelectMark(bool value)
        {
            if (value)
                _selectPanel.Open();
            else
                _selectPanel.Close();
        }

        private void HandleSlotSelected()
        {
            OnSlotSelectedEvent?.Invoke(_buildingDataSO);
            //SetSelectMark(true);

        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_buildingDataSO == null) return;
            _buildIconImage.sprite = _buildingDataSO.iconSprite;
            _buildNameText.text = _buildingDataSO.dataName;
        }
#endif
    }
}