using System.Collections.Generic;
using Project_Train.DataManage.CoreDataBaseSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Project_Train.UIManage.InGameSceneUI.DataBaseUIManage
{

    public class DatabaseDetailPanel : MonoBehaviour
    {
        [SerializeField] private Image _iconImage;
        [SerializeField] private TextMeshProUGUI _dataNameText;
        [SerializeField] private DataDetailSlot[] _slot;
        public void SetData(DataSO data)
        {
            _iconImage.sprite = data.iconSprite;
            _dataNameText.text = data.dataName;
            List<SerializableDetailDictionary.Entry> details = data.Details.Entries;

            for (int i = 0; i < _slot.Length; i++)
            {
                _slot[i].SetEnable(false);
            }

            for (int i = 0; i < details.Count; i++)
            {
                DataDetailSlot slot = _slot[(int)details[i].key];
                slot.SetEnable(true);
                slot.SetData(details[i].value);
            }
        }
    }
}