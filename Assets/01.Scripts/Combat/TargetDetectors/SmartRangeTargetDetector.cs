using Project_Train.Core.Attribute;
using UnityEngine;

namespace Project_Train.Combat.TargetDetectors
{
    public class SmartRangeTargetDetector : CapsuleTargetDetector
    {
        [Header("Additional Judgement Settings")]
        [SerializeField] private bool _useDistanceIgnore;
        [ShowIf(nameof(_useDistanceIgnore)), SerializeField, Range(0f, 1f)]
        private float _ignoreDistanceRatio = 0.15f;
        [ShowIf(nameof(_useDistanceIgnore)), SerializeField]
        private Color _ignoreGizmosColor = Color.cyan;

        [Space(10f)]
        [SerializeField] private bool _useDetectObstacle;
        [ShowIf(nameof(_useDetectObstacle)), SerializeField] 
        private LayerMask _detectObstacleLayer;

        // Inner Caching Arrays
        private RaycastHit[] _raycastHits = new RaycastHit[1];
        private Collider[] _validTargets = new Collider[32];
        private int _validTargetCount = 0;

        public override Collider[] DetectAllTargets()
        {
            _validTargetCount = 0;

            Vector3 bottom = CenterPosition + new Vector3(0f, -(_detectHeight * 0.5f), 0f);
            Vector3 top    = CenterPosition + new Vector3(0f,  (_detectHeight * 0.5f), 0f);

            _detectAmount = Physics.OverlapCapsuleNonAlloc(bottom, top, _detectRadius, _targets, _detectLayers);
            if (_detectAmount == 0) return null;

            for (int i = 0; i < _detectAmount; i++)
            {
                Collider collider = _targets[i];
                if (collider == null) continue;

                Vector3 dir = collider.transform.position - CenterPosition;
                float sqrDist = dir.sqrMagnitude;

                // Sequence : Check Distance
                float ignoreRadius = _detectRadius * _ignoreDistanceRatio;
                if (_useDistanceIgnore && sqrDist < ignoreRadius * ignoreRadius)
                    continue;

                // Sequence : Check Obstacles
                bool blocked = false;
                if (_useDetectObstacle)
                {
                    float distance = Mathf.Sqrt(sqrDist);
                    dir.Normalize();
                    int hitCount = Physics.RaycastNonAlloc(
                        CenterPosition, dir, _raycastHits, distance, _detectObstacleLayer
                    );
                    blocked = hitCount > 0;
                }

                if (!blocked)
                {
                    if (_validTargetCount < _validTargets.Length)
                        _validTargets[_validTargetCount++] = collider;
                }
            }

            if (_validTargetCount == 0) return null;

            Collider[] result = new Collider[_validTargetCount];
            for (int i = 0; i < _validTargetCount; i++)
                result[i] = _validTargets[i];

            return result;
        }

        public override Collider DetectClosestTarget()
        {
            Collider[] detected = DetectAllTargets();
            if (detected == null || detected.Length == 0) return null;

            Collider closest = null;
            float closestDist = float.MaxValue;

            for (int i = 0; i < detected.Length; i++)
            {
                float sqrDistance = (detected[i].bounds.center - CenterPosition).sqrMagnitude;
                if (sqrDistance < closestDist)
                {
                    closestDist = sqrDistance;
                    closest = detected[i];
                }
            }

            return closest;
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
