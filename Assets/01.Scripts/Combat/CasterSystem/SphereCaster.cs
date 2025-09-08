using UnityEngine;

namespace Project_Train.Combat.CasterSystem
{

    public class SphereCaster : Caster
    {
        [Space(5f)]
        [Header("Sphere Range Setting")]
        [SerializeField] private float _detectRadius = 1f;
        public float DetectRadius => _detectRadius;

        [ContextMenu("DebugCast")]
        public override void Cast()
        {
            base.Cast();
            int amount = Physics.OverlapSphereNonAlloc(CenterPosition, _detectRadius, _hits, _targetLayer);
            if (amount > 0)
            {
                ForceCast(_hits);
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = _gizmosColor;
            Gizmos.DrawWireSphere(CenterPosition, _detectRadius);
        }

#endif
    }
}