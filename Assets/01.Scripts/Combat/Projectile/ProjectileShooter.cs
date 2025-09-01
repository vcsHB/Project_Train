using UnityEngine;
namespace Project_Train.Combat.ProjectileSystem
{

    public class ProjectileShooter : MonoBehaviour
    {
        [Header("Projectile Setting")]
        // Pooling Type;
        [SerializeField] private float _lifeTime = 10f;
        [SerializeField] private float _speed = 10f;
        [SerializeField] private Vector3 _offset;


        public void Shoot(Vector3 direction)
        {
            StraightProjectile newProjectile = new(); // TODO: Pooling
            newProjectile.Shoot(transform.position + _offset, direction, _speed, _lifeTime);


            
        }


    }
}