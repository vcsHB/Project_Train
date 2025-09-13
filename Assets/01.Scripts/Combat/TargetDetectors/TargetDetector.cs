using Project_Train.Core.Attribute;
using UnityEngine;
namespace Project_Train.Combat
{

    public abstract class TargetDetector : MonoBehaviour
    {
        [SerializeField] protected LayerMask _detectLayers;
        [SerializeField] protected Vector3 _detectOffset;
        protected Vector3 CenterPosition => transform.position + _detectOffset;
        [SerializeField] protected int _detectMaxTargetAmount = 4;
        [Header("Gizmos Setting")]
        [SerializeField] protected bool _useGizmos;
        [ShowIf(nameof(_useGizmos)), SerializeField] protected Color _gizmosColor = Color.red;
        protected int _detectAmount;
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
            if (colliders == null) return null;
            if (_detectAmount > 0)
            {
                Collider closest = null;
                float minDistance = float.MaxValue;

                Vector2 currentPos = transform.position;
                for (int i = 0; i < _detectAmount; i++)
                {
                    Collider collider = colliders[i];
                    if (collider == null) continue;
                    float distance = Vector2.Distance(currentPos, collider.transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closest = collider;

                    }
                }
                return closest;

            }
            return null;
        }

        public abstract Collider DetectTarget();
        public abstract Collider[] DetectAllTargets();

    }
}