using Project_Train.Core.Attribute;
using UnityEngine;
namespace Project_Train.Combat
{

    public abstract class TargetDetector : MonoBehaviour
    {
        [SerializeField] protected LayerMask _detectLayers;
        [SerializeField] protected Vector3 _detectOffset;
        protected Vector3 CenterPosition => transform.position + _detectOffset;
        [Header("Gizmos Setting")]
        [SerializeField] protected bool _useGizmos;
        [ShowIf(nameof(_useGizmos)), SerializeField] protected Color _gizmosColor = Color.red;

        public void SetTargetLayers(LayerMask targetLayer)
        {
            _detectLayers = targetLayer;
        }

        public void AddTargetLayers(LayerMask newTargetLayer)
        {
            _detectLayers |= newTargetLayer;
        }
       
        public virtual Collider DetectClosestTarget()
        {
            Collider[] colliders = DetectAllTargets();

            Collider closest = null;
            float minDistance = float.MaxValue;

            Vector2 currentPos = transform.position;
            foreach (var collider in colliders)
            {
                float distance = Vector2.Distance(currentPos, collider.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closest = collider;
                }
            }

            return closest;
        }

        public abstract Collider DetectTarget();
        public abstract Collider[] DetectAllTargets();

    }
}