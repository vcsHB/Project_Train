using Project_Train.Combat.ProjectileSystem;
using Project_Train.Combat.TowerSystem.SubVisuals;
using UnityEngine;
namespace Project_Train.Combat.TowerSystem
{

    public class RocketLauncherGunBarrelPart : GunBarrelPart
    {
        [SerializeField] private InstantTargetProjectileShooter _shooter;
        [SerializeField] private RocketBarrelUnit[] _barrelUnits;
        private int _currentIndex;
        public override void Shoot()
        {
            _currentIndex = (_currentIndex + 1) % _barrelUnits.Length;
            RocketBarrelUnit currentBarrel = _barrelUnits[_currentIndex];
            currentBarrel.PlayVisualEffect();
            _shooter.Shoot(_targetData.targetTransform, _targetData.distanceToTarget);
        }
    }
}