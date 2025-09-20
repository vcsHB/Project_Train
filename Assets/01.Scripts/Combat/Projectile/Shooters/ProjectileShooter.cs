using Crogen.CrogenPooling;
using UnityEngine;
using UnityEngine.Events;
namespace Project_Train.Combat.ProjectileSystem
{

    public class ProjectileShooter : MonoBehaviour
    {
        public UnityEvent OnFireEvent;
        [Header("Projectile Setting")]
        [SerializeField] protected InGame_ProjectilePoolBasePoolType _projectileType;
        [SerializeField] protected float _lifeTime = 10f;
        [SerializeField] protected float _speed = 10f;
        [SerializeField] protected Vector3 _offset;

        public Vector3 FirePosition => transform.position + _offset;

        public virtual void SetOffset(Vector3 newOffset)
        {
            _offset = newOffset;
        }
    }
}