using UnityEngine;
namespace Project_Train.Combat.ProjectileSystem
{

    public class TargetProjectile : Projectile
    {
        public override void Shoot(Vector3 originPosition, Vector3 targetInfo, float speed, float lifeTime = 10)
        {
             _fireTime = Time.time;
        }

        


        
    }
}