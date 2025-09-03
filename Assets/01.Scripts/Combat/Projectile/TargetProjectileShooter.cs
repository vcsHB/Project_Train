using Crogen.CrogenPooling;
using UnityEngine;
namespace Project_Train.Combat.ProjectileSystem
{

    public class TargetProjectileShooter : ProjectileShooter
    {
        [SerializeField] private InGamePoolBasePoolType _projectilePoolingType;
        public void Shoot(Transform target)
        {
            TargetProjectile newProjectile = gameObject.Pop(_projectilePoolingType, transform.position, Quaternion.identity) as TargetProjectile; // TODO: Pooling
            newProjectile.SetForceTarget(target);
            Debug.Log("ASD");
            newProjectile.Shoot(transform.position + _offset, target.position, _speed, _lifeTime);

        }

    }
}