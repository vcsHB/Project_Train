using Crogen.CrogenPooling;
using Project_Train.Combat.CasterSystem;
using Project_Train.Core.Attribute;
using Project_Train.ObjectManage.VFX;
using UnityEngine;
namespace Project_Train.Combat.ProjectileSystem
{

    public abstract class Projectile : MonoBehaviour, IPoolingObject
    {
        [Header("Essential Settings")]
        [SerializeField] protected Caster _caster;
        [Header("Projectile Setting")]
        [SerializeField] protected bool _isHitDestroy;
        [SerializeField] protected InGamePoolBasePoolType _destroyVFX;

        [Header("Projectile LifeData")]
        [SerializeField, ReadOnly] protected float _lifeTime;
        [SerializeField] protected float _speed;
        [SerializeField, ReadOnly] protected Vector3 _originPosition;
        [SerializeField, ReadOnly] protected Vector3 _direction;
        protected Transform _forceTargetTrm;

        protected bool _isProjectileEnable;
        protected float _fireTime;

        [field: SerializeField] public string OriginPoolType { get; set; }
        GameObject IPoolingObject.gameObject { get; set; }

        protected virtual void Awake()
        {
            if (_isHitDestroy)
                _caster.OnCastSuccessEvent.AddListener(DestroyProjectile);

        }

        public virtual void OnPop()
        {
        }

        public virtual void OnPush()
        {
            _forceTargetTrm = null;
        }

        /// <summary>
        /// Set ForceTargeting To newTargetTrm
        /// </summary>
        public void SetForceTarget(Transform newTargetTrm)
        {
            _forceTargetTrm = newTargetTrm;
        }

        /// <summary>
        /// Shoot Projectile
        /// </summary>
        /// <param name="originPosition">
        /// launch start point
        /// </param>
        /// <param name="targetInfo">
        /// It can be the direction of the target or a specific location
        /// </param>
        /// <param name="speed">
        /// Bullet travel speed
        /// </param>
        /// <param name="lifeTime">
        /// Bullet Survive Time in Scene
        /// </param>
        public abstract void Shoot(Vector3 originPosition, Vector3 targetInfo, float speed, float lifeTime = 10f);


        protected virtual void DestroyProjectile()
        {
            _isProjectileEnable = false;
            // TODO : Return To Pool 
            // TODO : Destroy VFX
            VFXObject effect = gameObject.Pop(_destroyVFX, transform.position, Quaternion.identity)as VFXObject;
            effect.Play(transform.position, _direction);
            this.Push();
        }
    }
}