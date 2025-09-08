using UnityEngine;
using Crogen.CrogenPooling;
namespace Project_Train.Combat.ProjectileSystem
{

    public class InstantTargetProjectileShooter : ProjectileShooter
    {
        [SerializeField] private float _distanceHitDelayMultiplier = 1f;
        [SerializeField] private float _hitDelay = 0.3f;

        public void Shoot(Transform target, float distance)
        {
            InstantTargetProjectile projectile = gameObject.Pop(InGamePoolBasePoolType.RocketProjectile) as InstantTargetProjectile;
            if (projectile == null)
            {
                Debug.LogError("ProjectileType is Not Matched : ROCKETPROJECTILE");
                return;
            }
            projectile.SetForceTarget(target);
            projectile.Shoot(transform.position, target.position, 1f, _hitDelay + _distanceHitDelayMultiplier * distance * 0.1f);
            OnFireEvent?.Invoke();
        }
    }
}