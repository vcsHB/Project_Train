using System;
using UnityEngine;
using UnityEngine.Events;

namespace Project_Train.Combat.CasterSystem.HitBody
{
    public class Health : MonoBehaviour, IDamageable, IHealable, IDestroyable
    {
        public UnityEvent OnDieEvent;
        public event Action OnDamageIgnoreEvent;
        public event Action<float, float> OnHealthDecreaseEvent;
        public event Action<float, float> OnHealthIncreaseEvent;
        public event Action<DamageData> OnDamagedEvent;
        [SerializeField] private float _currentHealth;
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _hitResistanceDuration = 0.1f;
        [SerializeField] private bool _isResist;
        private float _hitAllowedTime;
        private bool _isDead;
        public bool IsDead => _isDead;
        public float CurrentHealth => _currentHealth;
        public float MaxHealth => _maxHealth;
        public bool IsResist => _isResist;


        #region External Functions

        public void SetResist(bool value)
        {
            _isResist = value;
        }
        public void FillHealthToMax()
        {
            _currentHealth = _maxHealth;

        }
        public void SetMaxHealth(float newMaxHealth, bool keepRatio = false)
        {
            if (keepRatio)
            {
                float ratio = Mathf.Clamp01(_currentHealth / _maxHealth);

                _maxHealth = newMaxHealth;
                _currentHealth = _maxHealth * ratio;
            }
            else
                _maxHealth = newMaxHealth;
            ClampHealth();
            HandleHealthChanged();

        }
        public HitResponse ApplyDamage(DamageData damageData)
        {
            if (Time.time > _hitAllowedTime || damageData.ignoreDamageCooltime)
            {
                OnDamagedEvent?.Invoke(damageData);
                _currentHealth -= damageData.damage;
                ClampHealth();
                HandleHealthChanged();
                OnHealthDecreaseEvent?.Invoke(_currentHealth, _maxHealth);
                return new HitResponse()
                {
                    isHit = true,
                };

            }

            return new HitResponse()
            {
                isHit = false,
            };
        }

        public void Restore(float amount)
        {
            if (amount > 0f)
            {
                _currentHealth += amount;
                ClampHealth();
                OnHealthIncreaseEvent?.Invoke(_currentHealth, _maxHealth);
                HandleHealthChanged();
            }
            else
            {
                Debug.LogWarning($"Health Restore Amount is Illegal Value : Value{amount}");
            }
        }

        public void ForceDestroy()
        {
            _currentHealth = 0f;
            OnHealthDecreaseEvent?.Invoke(_currentHealth, _maxHealth);
            HandleHealthChanged();

        }

        #endregion

        private void HandleHealthChanged()
        {
            if (_currentHealth <= 0f)
            {
                _isDead = true;
                OnDieEvent?.Invoke();
            }
        }

        private void ClampHealth()
        {
            _currentHealth = Mathf.Clamp(_currentHealth, 0f, _maxHealth);

        }


    }
}