using DG.Tweening;
using UnityEngine;
namespace Project_Train.Combat.ProjectileSystem
{

    public class TargetProjectile : Projectile
    {
        public override void Shoot(Vector3 originPosition, Vector3 targetInfo, float speed, float lifeTime = 10)
        {
            _fireTime = Time.time;
            _lifeTime = lifeTime;
            
            transform.position = originPosition;
            if (_forceTargetTrm != null)
            {
                targetInfo = _forceTargetTrm.position;
                Debug.Log("PositionSet");
            }
            transform.DOMove(targetInfo, _lifeTime).OnComplete(() =>
            {
                HandleProjectileArrive();
                Debug.Log("Destroyed");
            });
        }

        private void HandleProjectileArrive()
        {
            _caster.Cast();
            DestroyProjectile();
        }

    }
}