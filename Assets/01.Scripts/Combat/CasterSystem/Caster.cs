using Core.Attribute;
using UnityEngine;
using UnityEngine.Events;

namespace Project_Train.Combat.CasterSystem
{

    public class CasterData
    {
        // Data Capsule Grouping Class
    }

    public abstract class Caster : MonoBehaviour
    {
        [Tooltip("Calls by 1 Cast")]
        public UnityEvent OnCastEvent;
        [Tooltip("Calls when Successed Casting")]
        public UnityEvent OnCastSuccessEvent;

        [Space(10f)]
        [Header("Setting Values")]
        [SerializeField] protected Vector3 _offset;
        [SerializeField] protected LayerMask _targetLayer;
        [SerializeField] protected int _targetMaxAmount;

        [Header("Gizmos Setting")]
        [SerializeField] protected Color _gizmosColor = Color.red;

        // # LowCaster Array. Initialize in Awake()
        protected ICastable[] _casters;
        protected Collider[] _hits;

        public Vector3 CenterPosition => (Vector3)transform.position + _offset;

        protected virtual void Awake()
        {
            // GetComponent LowCasters
            _casters = GetComponentsInChildren<ICastable>();
        }

        public virtual void Cast()
        {
            OnCastEvent?.Invoke();
        }

        public void ForceCast(Collider[] hit)
        {
            for (int i = 0; i < hit.Length; i++)
                ForceCast(hit[i]);
        }

        public void ForceCast(Collider hit)
        {
            if (hit == null) return;

            for (int j = 0; j < _casters.Length; j++)
                _casters[j].Cast(hit);

            OnCastSuccessEvent?.Invoke();
        }

        public void SendCasterData(CasterData data)
        {
            foreach (ICastable caster in _casters)
                caster.HandleSetData(data);
        }

        public void SetTargetLayer(LayerMask newTargetLayer)
        {
            _targetLayer = newTargetLayer;
        }
        public void AddTargetLayer(LayerMask targetLayer)
        {
            _targetLayer |= targetLayer;
        }
    }
}