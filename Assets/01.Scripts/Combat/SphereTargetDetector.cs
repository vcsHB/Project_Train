using UnityEngine;
namespace Project_Train.Combat
{

    public class SphereTargetDetector : TargetDetector
    {
        [SerializeField] private float _detectRadius = 5f;
        protected Collider[] targets;

        public override Collider[] DetectAllTargets()
        {
            
            int amount = Physics.OverlapSphereNonAlloc(CenterPosition, _detectRadius, targets, _detectLayers);
            if (amount == 0) return null;
            return targets;

        }

        public override Collider DetectTarget()
        {
            int amount = Physics.OverlapSphereNonAlloc(CenterPosition, _detectRadius, targets, _detectLayers);
            if (amount == 0) return null;
            return targets[0];
        }


#if UNITY_EDITOR

        protected virtual void OnDrawGizmos()
        {
            if (!_useGizmos) return;
            Gizmos.color = _gizmosColor;
            Gizmos.DrawWireSphere(CenterPosition, _detectRadius);
        }
#endif
    }
}