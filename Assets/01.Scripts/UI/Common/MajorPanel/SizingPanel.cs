using UnityEngine;
namespace Project_Train.UIManage
{

    public abstract class SizingPanel : FadePanel
    {
        protected RectTransform _rectTrm;
        
        protected override void Awake()
        {
            base.Awake();
            _rectTrm = transform as RectTransform;

        }
        
    }
}