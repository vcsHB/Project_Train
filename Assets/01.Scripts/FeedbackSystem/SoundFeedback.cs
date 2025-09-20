using SoundManage;
using UnityEngine;

namespace Project_Train.FeedbackSystem
{

    public class SoundFeedback : Feedback
    {

        [SerializeField] private SoundSO _soundSO;

        public override void CreateFeedback()
        {
            SoundManage.SoundPlayer soundPlayer = SoundController.Instance.PlaySound(_soundSO, transform.position);
            if (soundPlayer == null)
            {
                Debug.LogError("SoundPlayer is Null.");
                return;
            }
        }

        public override void FinishFeedback()
        {
        }
    }
}