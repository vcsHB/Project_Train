using DG.Tweening;
using UnityEngine;
namespace Project_Train.BuildSystem
{

    public class HaloGroup : MonoBehaviour
    {
        [SerializeField] private float _enableDuration = 0.2f;
        
        public void SetEnable(bool enable)
        {
            transform.DOScale(enable ? 1f : 0f, _enableDuration);
        }
        
    }
}