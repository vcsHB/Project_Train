using UnityEngine;
namespace Project_Train.Combat.ProjectileSystem
{

    public class ProjectileShooter : MonoBehaviour
    {
        [Header("Projectile Setting")]
        // Pooling Type;
        [SerializeField] protected float _lifeTime = 10f;
        [SerializeField] protected float _speed = 10f;
        [SerializeField] protected Vector3 _offset;
    }
}