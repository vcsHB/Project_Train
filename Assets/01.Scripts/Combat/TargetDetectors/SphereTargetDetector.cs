using UnityEngine;
namespace Project_Train.Combat.TargetDetectors
{

    public class SphereTargetDetector : TargetDetector
    {
        [SerializeField] private float _detectRadius = 5f;
        protected Collider[] _targets;

        private void Awake()
        {
            _targets = new Collider[_detectMaxTargetAmount];
        }


        public override Collider[] DetectAllTargets()
        {

            _detectAmount = Physics.OverlapSphereNonAlloc(CenterPosition, _detectRadius, _targets, _detectLayers);
            if (_detectAmount == 0) return null;
            return _targets;

        }

        public override Collider DetectTarget()
        {
            _detectAmount = Physics.OverlapSphereNonAlloc(CenterPosition, _detectRadius, _targets, _detectLayers);
            if (_detectAmount == 0) return null;
            return _targets[0];
        }


#if UNITY_EDITOR

        protected virtual void OnDrawGizmosSelected()
        {
            if (!_useGizmos) return;
            Gizmos.color = new Color(_gizmosColor.r, _gizmosColor.g, _gizmosColor.b, 0.2f);
            Gizmos.DrawSphere(CenterPosition, _detectRadius);
            Gizmos.color = _gizmosColor;
            Gizmos.DrawWireSphere(CenterPosition, _detectRadius);
            

        }
#endif
    }
}