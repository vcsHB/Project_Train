using DG.Tweening;
using UnityEngine;
namespace Project_Train.Combat.ProjectileSystem
{

    public class StraightProjectile : Projectile
    {


        private void FixedUpdate()
        {
            if (_isProjectileEnable)
            {
                _caster.Cast();
            }
        }


        #region External Functions

        public override void Shoot(Vector3 originPosition, Vector3 targetInfo, float speed, float lifeTime = 10f)
        {
            // Life Record
            _fireTime = Time.time;

            _direction = targetInfo.normalized;
            _originPosition = originPosition;
            _speed = speed;
            _lifeTime = lifeTime;
            _caster.ClearCastRecord();

            transform.position = _originPosition;
            transform.DOMove(_originPosition + _direction * _lifeTime * _speed, _lifeTime).OnComplete(() =>
            {
                DestroyProjectile();
            });
            _isProjectileEnable = true;
        }
        #endregion
    }
}