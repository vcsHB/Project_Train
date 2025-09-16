using UnityEngine;
namespace Project_Train.Combat.ProjectileSystem
{

    public class GatlingProjectile : StraightProjectile
    {
        [SerializeField] private Transform _visualTrm;
        public override void Shoot(Vector3 originPosition, Vector3 targetInfo, float speed, float lifeTime = 10)
        {
            base.Shoot(originPosition, targetInfo, speed, lifeTime);
            _visualTrm.up = _direction;
        }
    }
}