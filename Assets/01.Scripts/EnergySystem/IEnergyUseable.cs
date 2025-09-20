using UnityEngine;
namespace Project_Train.EnergySystem
{

    public interface IEnergyUseable
    {
        public bool UseEnergy(float amount);
        public void ForceUseEnergy(float amount);
    }
}