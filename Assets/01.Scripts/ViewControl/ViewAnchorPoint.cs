using UnityEngine;
using UnityEngine.Events;
namespace Project_Train.ViewControl
{
    
    public class ViewAnchorPoint : MonoBehaviour
    {
        
        public UnityEvent OnEnterEvent;
        public UnityEvent OnExitEvent;
        public Vector3 PointWorldPosition => transform.position;


        public virtual void Enter()
        {
            OnEnterEvent?.Invoke();
        }

        public virtual void Exit()
        {
            OnExitEvent?.Invoke();
        }
    }
}