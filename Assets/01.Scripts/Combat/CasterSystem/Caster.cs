using System.Collections.Generic;
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
        [SerializeField] private bool _isDuplicateIgnore;

        private readonly HashSet<Collider> _castRecord = new();

        [Header("Gizmos Setting")]
        [SerializeField] protected Color _gizmosColor = Color.red;



        // # LowCaster Array. Initialize in Awake()
        protected ICastable[] _casters;
        protected Collider[] _hits;

        public Vector3 CenterPosition => transform.position + _offset;

        protected virtual void Awake()
        {
            // GetComponent LowCasters
            _hits = new Collider[_targetMaxAmount];
            _casters = GetComponentsInChildren<ICastable>();
        }

        #region External Caster


        public virtual void Cast()
        {
            OnCastEvent?.Invoke();
        }

        public void ClearCastRecord()
        {
            if (_isDuplicateIgnore)
                _castRecord.Clear();
        }

        public void ForceCast(Collider[] hit)
        {
            if (hit == null) return;
            for (int i = 0; i < hit.Length; i++)
                ForceCast(hit[i]);
        }

        public void ForceCast(Collider hit)
        {
            if (hit == null) return;

            if (_isDuplicateIgnore && !_castRecord.Add(hit))
                return;

            for (int i = 0; i < _casters.Length; i++)
            {
                _casters[i].Cast(hit);
            }

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
        #endregion
    }
}