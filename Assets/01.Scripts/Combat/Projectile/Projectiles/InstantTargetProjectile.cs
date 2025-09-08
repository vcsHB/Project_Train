using UnityEngine;
namespace Project_Train.Combat.ProjectileSystem
{

    public class InstantTargetProjectile : Projectile
    {
        private float _arriveTime;
        private Vector3 _exceptionPosition;

        public override void Shoot(Vector3 originPosition, Vector3 targetInfo, float speed, float lifeTime = 10)
        {
            // LifeTime = Delay;
            transform.position = originPosition;
            _exceptionPosition = targetInfo;
            _lifeTime = lifeTime;
            _arriveTime = Time.time + _lifeTime;
            Invoke(nameof(HandleArrive), _lifeTime);
            _isProjectileEnable = true;
        }

        private void Update()
        {
            if (_arriveTime < Time.time && _isProjectileEnable)
            {
                _isProjectileEnable = false;
                HandleArrive();
            }
        }

        private void HandleArrive()
        {
            if (_forceTargetTrm == null)
                transform.position = _exceptionPosition;
            else
                transform.position = _forceTargetTrm.position;

            _caster.Cast();
            DestroyProjectile();
        }
    }
}