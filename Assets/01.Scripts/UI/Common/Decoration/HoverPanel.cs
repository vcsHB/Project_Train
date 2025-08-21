using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
namespace Project_Train.UIManage
{

    public class HoverPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public UnityEvent OnMouseEnterEvent;
        public UnityEvent OnMouseExitEvent;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            OnMouseEnterEvent?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnMouseExitEvent?.Invoke();
        }
    }
}