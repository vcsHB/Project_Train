using UnityEngine;
namespace Project_Train.Combat.ProjectileSystem
{

    public class InstantTargetProjectile : Projectile
    {
        private float _arriveTime;

        public override void Shoot(Vector3 originPosition, Vector3 targetInfo, float speed, float lifeTime = 10)
        {
            // LifeTime = Delay;
            transform.position = originPosition;
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
            transform.position = _forceTargetTrm.position;
            _caster.Cast();
            DestroyProjectile();
        }
    }
}