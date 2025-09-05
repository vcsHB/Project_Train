using Project_Train.Core.Attribute;
using UnityEngine;

namespace Project_Train.Combat.TargetDetectors
{
    public class SmartRangeTargetDetector : CapsuleTargetDetector
    {
        [Header("Additional Judgement Settings")]

        [SerializeField] private bool _useDistanceIgnore;
        [ShowIf(nameof(_useDistanceIgnore)), SerializeField, Range(0f, 1f)] private float _ignoreDistanceRatio = 0.15f;
        [ShowIf(nameof(_useDistanceIgnore)), SerializeField] private Color _ignoreGizmosColor = Color.cyan;

        [SerializeField] private LayerMask _detectObstacleLayer;

        public override Collider[] DetectAllTargets()
        {
            Vector3 bottom = CenterPosition + new Vector3(0f, -(_detectHeight * 0.5f), 0f);
            Vector3 top = CenterPosition + new Vector3(0f, _detectHeight * 0.5f, 0f);
            int amount = Physics.OverlapCapsuleNonAlloc(bottom, top, _detectRadius, targets, _detectLayers);
            if (amount == 0) return null;
            Physics.CheckSphere(transform.position, _detectRadius, _detectLayers);
            return targets;
        }

        public override Collider DetectClosestTarget()
        {
            return base.DetectClosestTarget();
        }


#if UNITY_EDITOR

        protected override void OnDrawGizmosSelected()
        {
            if (!_useGizmos) return;
            base.OnDrawGizmosSelected();
            if (_useDistanceIgnore)
            {
                UnityEditor.Handles.color = _ignoreGizmosColor;
                UnityEditor.Handles.DrawWireDisc(CenterPosition, Vector3.up, _detectRadius * _ignoreDistanceRatio);
                Color ignoreInnerColor = _ignoreGizmosColor;
                ignoreInnerColor.a = 0.2f;
                UnityEditor.Handles.color = ignoreInnerColor;
                UnityEditor.Handles.DrawSolidDisc(CenterPosition, Vector3.up, _detectRadius * _ignoreDistanceRatio);


            }
        }

#endif


    }

}