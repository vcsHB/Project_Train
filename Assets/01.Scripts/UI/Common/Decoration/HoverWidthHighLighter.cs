using DG.Tweening;
using UnityEngine;
namespace Project_Train.UIManage.Decoration
{

    public class HoverWidthHighLighter : HoverPanel
    {
        [SerializeField] private float _defaultWidth = 100f;
        [SerializeField] private float _enableWidth = 300f;
        [SerializeField] private float _duration = 0.2f;
        [SerializeField] private bool _useUnscaledTime;
        private RectTransform _rectTrm;
        private float _panelYSize;
        private void Awake()
        {
            _rectTrm = transform as RectTransform;
            _panelYSize = _rectTrm.sizeDelta.y;
            OnMouseEnterEvent.AddListener(HandleMouseEnter);
            OnMouseExitEvent.AddListener(HandleMouseExit);
        }

        private void HandleMouseEnter()
        {
            _rectTrm.DOSizeDelta(new Vector2(_enableWidth, _panelYSize), _duration).SetUpdate(_useUnscaledTime);
        }

        private void HandleMouseExit()
        {
            _rectTrm.DOSizeDelta(new Vector2(_defaultWidth, _panelYSize), _duration).SetUpdate(_useUnscaledTime);
        }
    }
}