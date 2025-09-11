using Project_Train.DataManage.CoreDataBaseSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Project_Train.UIManage.InGameSceneUI.DataBaseUIManage
{

    public class RequireResourceContent : MonoBehaviour
    {
        [SerializeField] private Image _resourceIconImage;
        [SerializeField] private TextMeshProUGUI _resourceNameText;
        [SerializeField] private TextMeshProUGUI _resourceAmountText;

        public void SetResourceData(ResourceDataSO resourceData, int amount)
        {
            _resourceIconImage.sprite = resourceData.iconSprite;
            _resourceNameText.text = resourceData.dataName;
            _resourceAmountText.text = amount.ToString();
        }

        public void SetEnable(bool value)
        {
            gameObject.SetActive(value);
        }
    }
}