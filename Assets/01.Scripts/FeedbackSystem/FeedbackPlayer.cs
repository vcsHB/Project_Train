using UnityEngine;

namespace Project_Train.FeedbackSystem
{

    public class FeedbackPlayer : MonoBehaviour
    {
        private Feedback[] _feedbacks;
        private void Awake()
        {
            _feedbacks = GetComponents<Feedback>();
        }

        public void CreateFeedback()
        {
            if(_feedbacks == null) return;
            for (int i = 0; i < _feedbacks.Length; i++)
            {
                _feedbacks[i].CreateFeedback();
            }
        }

        public void StopFeedback()
        {
            for (int i = 0; i < _feedbacks.Length; i++)
            {
                _feedbacks[i].FinishFeedback();
            }
        }
    }

}
