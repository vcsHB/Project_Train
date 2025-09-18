using UnityEngine;
namespace Project_Train.Combat.ProjectileSystem
{

    public class StraightProjectile : Projectile
    {

        private float _destroyTime;
        protected virtual void FixedUpdate()
        {
            if (_isProjectileEnable)
            {
                _caster.Cast();
                transform.position += _direction * Time.fixedDeltaTime * _speed;
                if (_destroyTime < Time.time)
                {
                    DestroyProjectile();
                    _isProjectileEnable = false;
                }
            }
        }


        #region External Functions

        public override void Shoot(Vector3 originPosition, Vector3 targetInfo, float speed, float lifeTime = 10f)
        {
            // Life Record
            _fireTime = Time.time;
            _destroyTime = _fireTime + lifeTime;
            _lifeTime = lifeTime;

            if (_forceTargetTrm == null)
            {
                _direction = targetInfo.normalized;
            }
            else
            {
                _direction = _forceTargetTrm.position - originPosition;
                _direction.Normalize();
            }
            _originPosition = originPosition;
            _speed = speed;
            _caster.ClearCastRecord();

            transform.position = _originPosition;

            _isProjectileEnable = true;
        }
        #endregion
    }
}