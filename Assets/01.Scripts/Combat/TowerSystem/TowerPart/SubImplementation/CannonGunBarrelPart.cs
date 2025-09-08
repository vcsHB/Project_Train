using Project_Train.Combat.ProjectileSystem;
using UnityEngine;
namespace Project_Train.Combat.TowerSystem
{

    public class CannonGunBarrelPart : GunBarrelPart
    {
        [SerializeField] protected TargetProjectileShooter _shooter;
        public override void Shoot()
        {
            _shooter.Shoot(_targetData.targetTransform, _targetData.distanceToTarget);
        }
    }
}