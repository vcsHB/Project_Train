using Crogen.CrogenPooling;
using Project_Train.ObjectManage.VFX;
using UnityEngine;
namespace Project_Train.FeedbackSystem
{

    public class VFXPlayFeedback : Feedback
    {
        [SerializeField] private InGamePoolBasePoolType _vfxPoolingType;
        public override void CreateFeedback()
        {
            VFXObject vfx = gameObject.Pop(_vfxPoolingType) as VFXObject;
            vfx.Play(transform.position, Vector3.zero);
        }

        public override void FinishFeedback()
        {
        }
    }
}