using System;
using System.Collections.Generic;
using UnityEngine;
namespace Project_Train.Combat.StatSystem
{
    [CreateAssetMenu(fileName = "StatSO", menuName = "SO/StatSystem/StatSO")]
    public class StatSO : ScriptableObject, ICloneable
    {
        public delegate void ValueChangeHandler(StatSO stat, float currentValue, float prevValue);
        public event ValueChangeHandler OnValuechange;
        public StatusEnumType statType;
        public string statName;
        public string description;
        public string displayName;
        [SerializeField] private Sprite _icon;
        [SerializeField] private float _baseValue;
        [SerializeField] private float _minValue;
        [SerializeField] private float _maxValue;
        private float _totalValue = 0;

        private Dictionary<object, float> _modifyValueByKey = new Dictionary<object, float>();


        #region Properties

        public Sprite Icon => _icon;

        public float MaxValue => _maxValue;
        public float MinValue => _minValue;
        private bool _isValueChanged;

        public float Value
        {
            get
            {
                if (_isValueChanged)
                {
                    _isValueChanged = false;
                    foreach (float value in _modifyValueByKey.Values)
                        _totalValue += value;
                }
                return _totalValue;
            }
        }
        public bool IsMax => Mathf.Approximately(Value, _maxValue);
        public bool IsMin => Mathf.Approximately(Value, _minValue);

        #endregion


        public void AddModifier(object key, float value)
        {
            float previousValue = Value;
            _isValueChanged = true;
            if (_modifyValueByKey.ContainsKey(key))
            {
                _modifyValueByKey[key] += value;
            }
            else
            {
                _modifyValueByKey.Add(key, value);
            }
            TryInvokeValueChangeEvent(Value, previousValue);

        }

        public void RemoveModifier(object key)
        {
            float previousValue = Value;
            _isValueChanged = true;
            if (_modifyValueByKey.ContainsKey(key))
            {
                _modifyValueByKey.Remove(key);
            }
            else
            {
                Debug.LogWarning($"This KEY [{key}] is Contain in modifiers");
            }
            TryInvokeValueChangeEvent(Value, previousValue);
        }

        public void ClearModifier()
        {
            _isValueChanged = true;
            float previousValue = Value;
            _modifyValueByKey.Clear();
            TryInvokeValueChangeEvent(Value, previousValue);
        }

        private void TryInvokeValueChangeEvent(float currentValue, float prevValue)
        {
            if (!Mathf.Approximately(currentValue, prevValue))
            {
                OnValuechange?.Invoke(this, currentValue, prevValue);
            }
        }

        public virtual object Clone() => Instantiate(this);
    }
}