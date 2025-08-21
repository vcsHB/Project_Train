using UnityEngine;
namespace Project_Train.Combat
{

    public class CapsuleDetector : TargetDetector
    {
        [SerializeField] private float _detectRadius = 5f;
        [SerializeField] private float _detectHeight = 20f;
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


    }
}