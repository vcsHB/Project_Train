using System;
using UnityEngine;
using UnityEngine.Events;

namespace Project_Train.Combat.CasterSystem
{
    public class DamageCaster : MonoBehaviour, ICastable
    {
        public UnityEvent OnCastSuccessEvent;
        public event Action<DamageData> OnCastCombatDataEvent;
        [SerializeField] private DamageData _damageData;

        public void Cast(Collider target)
        {
            if (target.TryGetComponent(out IDamageable damageable))
            {
                damageable.ApplyDamage(_damageData);
                InvokeCastEvent(ref _damageData);
            }
        }
        protected void InvokeCastEvent(ref DamageData data)
        {
            OnCastCombatDataEvent?.Invoke(data);
            OnCastSuccessEvent?.Invoke();
        }

        public void HandleSetData(CasterData data)
        {
        }
    }
}