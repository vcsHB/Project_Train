using CameraControllers;
using UnityEngine;
namespace Project_Train.FeedbackSystem
{
    public class CameraShakingFeedback : Feedback
    {
        [SerializeField] private float _power;
        [SerializeField] private float _duration;
        private CameraShakeController _cameraShakeController;

        private void Start()
        {
            _cameraShakeController = CameraManager.Instance.GetCompo<CameraShakeController>();
        }
        public override void CreateFeedback()
        {
            _cameraShakeController.Shake(_power, _duration);
        }

        public override void FinishFeedback()
        {

        }
    }
}