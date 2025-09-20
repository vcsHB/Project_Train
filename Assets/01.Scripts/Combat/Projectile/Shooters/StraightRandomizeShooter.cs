using Crogen.CrogenPooling;
using UnityEngine;
namespace Project_Train.Combat.ProjectileSystem
{

    public class StraightRandomizeShooter : ProjectileShooter
    {
        [Header("Randomize Settings")]
        [SerializeField, Range(0f, 1f)] private float _randomizeLevel = 0.05f;

        public void Shoot(Vector3 targetDirection)
        {
            targetDirection.Normalize();
            StraightProjectile projectile = gameObject.Pop(_projectileType, FirePosition, Quaternion.identity) as StraightProjectile;
            Vector3 newDirection = targetDirection * 5f + (Random.insideUnitSphere * _randomizeLevel);
            newDirection.Normalize();
            projectile.Shoot(FirePosition, newDirection, _speed, _lifeTime);
            OnFireEvent?.Invoke();

        }

    }
}