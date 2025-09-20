using System;
using UnityEngine;
namespace Project_Train.EnergySystem
{

    public class Battery : MonoBehaviour, IEnergyChargeable, IEnergyUseable
    {

        public event Action<float, float> OnEnergyValueChangeEvent;
        [SerializeField] private float _currentEnergy;
        [SerializeField] private float _maxEnergy;

        // TODO : Todo After for Reactor....

        #region External Implemented Functions
        public void Charge(float amount)
        {
            _currentEnergy += amount;
            InvokeEnergyValueChangeEvents();
            ClampEnergyCapacity();
        }

        public void ForceUseEnergy(float amount)
        {
            _currentEnergy -= amount;
            InvokeEnergyValueChangeEvents();
            ClampEnergyCapacity();

        }

        public bool UseEnergy(float amount)
        {
            if (_currentEnergy < amount) return false;

            _currentEnergy -= amount;
            InvokeEnergyValueChangeEvents();
            ClampEnergyCapacity();
            return true;
        }

        #endregion


        private void ClampEnergyCapacity()
        {
            _currentEnergy = Mathf.Clamp(_currentEnergy, 0f, _maxEnergy);

        }
        private void InvokeEnergyValueChangeEvents()
        {
            OnEnergyValueChangeEvent?.Invoke(_currentEnergy, _maxEnergy);

        }


    }
}