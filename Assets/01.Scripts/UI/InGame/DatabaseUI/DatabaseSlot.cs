using System;
using Project_Train.DataManage.CoreDataBaseSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Project_Train.UIManage.InGameSceneUI.DataBaseUIManage
{

    public class DatabaseSlot : MonoBehaviour
    {

        public Action<DataSO> OnSlotSelectEvent;
        [SerializeField] private Image _dataIconImage;

        [SerializeField] private CanvasGroup _lockPanel;
        private DataSO _data;
        private Button _button;
        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(HandleSlotSelect);
        }

        private void HandleSlotSelect()
        {
            OnSlotSelectEvent?.Invoke(_data);
        }

        public void SetData(DataSO dataSO, bool isUnlock)
        {
            _data = dataSO;
            _dataIconImage.sprite = dataSO.iconSprite;
            _lockPanel.alpha = !isUnlock ? 1f : 0f;
            _lockPanel.interactable = !isUnlock;
            _lockPanel.blocksRaycasts = !isUnlock;
        }
    }
}