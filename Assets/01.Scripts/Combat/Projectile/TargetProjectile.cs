using DG.Tweening;
using UnityEngine;
namespace Project_Train.Combat.ProjectileSystem
{

    public class TargetProjectile : Projectile
    {
        private float _currentLifeTime = 0f;

        public override void OnPop()
        {
            base.OnPop();
            _currentLifeTime = 0f;
        }
        public override void Shoot(Vector3 originPosition, Vector3 targetInfo, float speed, float lifeTime = 10)
        {
            _fireTime = Time.time;
            _lifeTime = lifeTime;
            _originPosition = originPosition;
            _isProjectileEnable = true;
            
            if (_forceTargetTrm == null)
                Debug.LogError("[Missing Target Error] Must Be Bind ForceTargetTrm to TargetProjectile.");

        }

        private void Update()
        {
            if(_isProjectileEnable)
            _currentLifeTime += Time.deltaTime;
            if (_currentLifeTime > _lifeTime)
            {
                HandleProjectileArrive();
                return;
            }
            float ratio = _currentLifeTime / _lifeTime;
            transform.position = Vector3.Lerp(_originPosition, _forceTargetTrm.position, ratio);


        }

        private void HandleProjectileArrive()
        {
            _caster.Cast();
            DestroyProjectile();

        }

    }
}