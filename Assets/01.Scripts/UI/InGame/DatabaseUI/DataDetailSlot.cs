using DG.Tweening;
using Project_Train.DataManage.CoreDataBaseSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Project_Train.UIManage.InGameSceneUI
{

    public abstract class DataDetailSlot : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI _titleText;
        [SerializeField] protected float _activateDuration = 0.2f;
        protected Image _panelBackgroundImage;

        protected virtual void Awake()
        {
            _panelBackgroundImage = GetComponent<Image>();

        }

        public void SetEnable(bool value)
        {
            gameObject.SetActive(value);
            if (value)
            {
                _panelBackgroundImage.fillAmount = 0f;
                _panelBackgroundImage.DOFillAmount(1f, _activateDuration).SetUpdate(true);
            }

        }
        public abstract void SetData(DataDetailSO detail);

    }
}