using Project_Train.Combat.ProjectileSystem;
using UnityEngine;
namespace Project_Train.Combat.TowerSystem
{
    public class GatlingGunBarrelPart : GunBarrelPart
    {
        [SerializeField] private StraightRandomizeShooter _shooter;
        [SerializeField] private float _rotationSpeed = 5f;
        [SerializeField] private Transform _rotationHandleTrm;

        public override void Shoot()
        {
            if (_rotationHandleTrm == null) return;
            _rotationHandleTrm.Rotate(0f, 0f, -_rotationSpeed, Space.Self);
            _shooter.Shoot(_targetData.targetDirection);
        }
    }
}
