using Crogen.CrogenPooling;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
namespace Project_Train.ObjectManage.VFX
{

    public abstract class VFXObject : MonoBehaviour, IPoolingObject
    {
        public string OriginPoolType { get; set; }
        GameObject IPoolingObject.gameObject { get; set; }

        [SerializeField] protected float _lifeTime;
        protected Vector3 _position;
        protected Vector3 _direction;
        protected float _disableTime;
        public bool IsEffectEnable { get; protected set; }

        public void Play(Vector3 position, Vector3 direction)
        {
            _position = position;
            _direction = direction;
            PlayEffect();
        }

        protected abstract void PlayEffect();
        public virtual void OnPop()
        {
            _disableTime = Time.time + _lifeTime;
            IsEffectEnable = true;

        }

        public virtual void OnPush()
        {
            IsEffectEnable = false;
        }

        protected virtual void Update()
        {
            if (IsEffectEnable && Time.time > _disableTime)
            {
                HandleDestroyEffect();
            }
        }

        protected virtual void HandleDestroyEffect()
        {
            this.Push();
        }


    }
}