using UnityEngine;
namespace Project_Train.Combat
{

    public class CapsuleTargetDetector : TargetDetector
    {
        [SerializeField] protected float _detectRadius = 5f;
        [SerializeField] protected float _detectHeight = 20f;
        protected Collider[] targets;

        public override Collider[] DetectAllTargets()
        {

            Vector3 bottom = CenterPosition + new Vector3(0f, -(_detectHeight * 0.5f), 0f);
            Vector3 top = CenterPosition + new Vector3(0f, _detectHeight * 0.5f, 0f);
            int amount = Physics.OverlapCapsuleNonAlloc(bottom, top, _detectRadius, targets, _detectLayers);
            if (amount == 0) return null;
            return targets;

        }

        public override Collider DetectTarget()
        {
            Vector3 bottom = CenterPosition + new Vector3(0f, -(_detectHeight * 0.5f), 0f);
            Vector3 top = CenterPosition + new Vector3(0f, _detectHeight * 0.5f, 0f);
            int amount = Physics.OverlapCapsuleNonAlloc(bottom, top, _detectRadius, targets, _detectLayers);
            if (amount == 0) return null;
            return targets[0];
        }

#if UNITY_EDITOR


        protected virtual void OnDrawGizmosSelected()
        {
            if (!_useGizmos) return;

            UnityEditor.Handles.color = _gizmosColor;
            Color innerColor = _gizmosColor;
            innerColor.a = 0.2f;
            UnityEditor.Handles.DrawWireDisc(CenterPosition, Vector3.up, _detectRadius);
            UnityEditor.Handles.color = innerColor;
            UnityEditor.Handles.DrawSolidDisc(CenterPosition, Vector3.up, _detectRadius);
        }
#endif


    }
}