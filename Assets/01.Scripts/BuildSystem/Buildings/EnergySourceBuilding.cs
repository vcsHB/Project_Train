using Project_Train.EnergySystem;
using UnityEngine;
namespace Project_Train.BuildSystem
{

    public class EnergySourceBuilding : Building
    {
        public EnergyGenerator EnergyGenerator { get; protected set; }
        protected override void Awake()
        {
            base.Awake();
            EnergyGenerator = GetComponentInChildren<EnergyGenerator>();
            if (EnergyGenerator == null)
            {
                Debug.LogError("Not Attached EnergyGenerator in EnergySourceBuilding");
                return;
            }
            EnergyGenerator.SupplyEnergy();

        }

        private void OnDestroy()
        {
            if (EnergyGenerator != null)
                EnergyGenerator.SubtractEnergy();
        }
    }
}