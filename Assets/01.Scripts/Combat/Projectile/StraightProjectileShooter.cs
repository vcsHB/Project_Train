using UnityEngine;
namespace Project_Train.Combat.ProjectileSystem
{

    public class StraightProjectileShooter : ProjectileShooter
    {
       


        public void Shoot(Vector3 targetInfo)
        {
            StraightProjectile newProjectile = new(); // TODO: Pooling
            newProjectile.Shoot(transform.position + _offset, targetInfo, _speed, _lifeTime);

        }
        


    }
}