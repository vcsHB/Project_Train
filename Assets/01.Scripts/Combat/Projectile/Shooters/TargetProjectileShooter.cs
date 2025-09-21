using Crogen.CrogenPooling;
using UnityEngine;
namespace Project_Train.Combat.ProjectileSystem
{

    public class TargetProjectileShooter : ProjectileShooter
    {
        [SerializeField] private float _lifeTimeMultiplier = 0.2f;

        public void Shoot(Transform target, float distance)
        {
            TargetProjectile newProjectile = gameObject.Pop(_projectileType, transform.position, Quaternion.identity) as TargetProjectile; // TODO: Pooling
            newProjectile.SetForceTarget(target);
            newProjectile.Shoot(transform.position + _offset, target.position, _speed, _lifeTime * distance * _lifeTimeMultiplier);
            OnFireEvent?.Invoke();
        }

    }
}