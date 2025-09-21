using System;
using Project_Train.BuildSystem;
using Project_Train.Combat.TargetDetectors;
using UnityEngine;
using UnityEngine.Events;
namespace Project_Train.Combat.TowerSystem
{
    [System.Serializable]
    public class TargetData
    {
        public Transform targetTransform;
        public Vector3 targetDirection;
        public float distanceToTarget;

    }
    public abstract class Tower : Building
    {
        public UnityEvent OnAttackUnityEvent;
        public event Action OnAttackEvent;
        public event Action OnTargetDetectedEvent;

        [Header("Essential Settings")]
        [SerializeField] protected TowerHead _head;
        [SerializeField] protected SmartRangeTargetDetector _targetDetector;


        [Header("Tower Status Settings")]
        [SerializeField] protected bool _orderMode;
        [SerializeField] protected TargetData _targetData = new();

        [Header("Tower Attack Settings")]
        [SerializeField] protected float _fireTerm;
        protected float _nextFireTime;
        protected override void Awake()
        {
            base.Awake();
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
                Vector3 direction = target.transform.position - _targetDetector.transform.position;
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
                        InvokeAttackEvent();
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
            OnAttackUnityEvent?.Invoke();
        }

        protected void InvokeTargetDetectedEvent()
        {
            OnTargetDetectedEvent?.Invoke();
        }

#if UNITY_EDITOR


        public void SetDetectRange(float newRange, float ignoreRange)
        {
            if (_targetDetector == null)
            {
                Debug.LogError("[Auto Value Setting Error] TargetDetector is not Binded");
            }

            _targetDetector.SetDetectRange(newRange, ignoreRange);
            UnityEditor.EditorUtility.SetDirty(this);
            if (_targetDetector != null)
                UnityEditor.EditorUtility.SetDirty(_targetDetector);
            //Debug.Log("Tower:SetDetectRange");

        }
#endif


    }
}