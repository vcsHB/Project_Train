using System;
using UnityEngine;
namespace Project_Train.Combat.TowerSystem
{
    [System.Serializable]
    public class TargetData
    {
        public Transform targetTransform;
        public Vector3 targetDirection;
        public float distanceToTarget;

    }
    public abstract class Tower : MonoBehaviour
    {
        public event Action OnAttackEvent;
        public event Action OnTargetDetectedEvent;

        [Header("Essential Settings")]
        [SerializeField] protected TowerHead _head;
        [SerializeField] protected TargetDetector _targetDetector;


        [Header("Tower Status Settings")]
        [SerializeField] protected bool _orderMode;
        [SerializeField] protected TargetData _targetData = new();

        [Header("Tower Attack Settings")]
        [SerializeField] protected float _fireTerm;
        protected float _nextFireTime;
        private void Awake()
        {
            _head.Initialize(_targetData);
        }

        private void Update()
        {
            Collider target = null;
            if (_orderMode)
                target = _targetDetector.DetectClosestTarget();

            if (target != null)
            {
                // Non Detected
                if (_targetData.targetTransform == null)
                    InvokeTargetDetectedEvent();
                _targetData.targetTransform = target.transform;
                Vector2 direction = target.transform.position - transform.position;
                _targetData.distanceToTarget = direction.magnitude;
                direction.Normalize();
                _targetData.targetDirection = direction;
                bool isAimAligned = _head.SetAimRotation(target.transform.position);

                if (isAimAligned)
                {
                    if (_nextFireTime < Time.time)
                    {
                        _nextFireTime = Time.time + _fireTerm;
                        TryShoot();

                    }
                }
            }
            else
            {
                _targetData.targetTransform = null;

            }

        }

        protected abstract void TryShoot();
        protected void InvokeAttackEvent()
        {
            OnAttackEvent?.Invoke();
        }

        protected void InvokeTargetDetectedEvent()
        {
            OnTargetDetectedEvent?.Invoke();
        }

    }
}