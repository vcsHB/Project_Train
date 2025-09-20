using Project_Train.BuildSystem;
using Project_Train.Combat.CasterSystem.HitBody;
using TMPro;
using UnityEngine;
namespace Project_Train.UIManage.InGameSceneUI.EngineeringUI
{

    public class StructureInformationPanel : MonoBehaviour
    {
        [SerializeField] private BuildingHealthGauge _healthGauge;
        [SerializeField] private TextMeshProUGUI _structureNameText;
        private Building _currentSelectedBuilding;
        private Health _buildingHealth;


        public void SetStructureData(Building targetBuilding)
        {


            if (_currentSelectedBuilding != null)
            {

                _buildingHealth.OnHealthDecreaseEvent -= _healthGauge.HandleGaugeRefresh;
                _buildingHealth.OnHealthIncreaseEvent -= _healthGauge.HandleGaugeRefresh;

            }

            _currentSelectedBuilding = targetBuilding;
            _buildingHealth = targetBuilding.HealthCompo;
            _buildingHealth.OnHealthDecreaseEvent += _healthGauge.HandleGaugeRefresh;
            _buildingHealth.OnHealthIncreaseEvent += _healthGauge.HandleGaugeRefresh;
            _healthGauge.ResetHealthGauge();
            _healthGauge.HandleGaugeRefresh(_buildingHealth.CurrentHealth, _buildingHealth.MaxHealth);

            if (targetBuilding.BuildingData == null)
            {
                Debug.LogError($"BuildingData is not Binded (name:{targetBuilding.gameObject.name})");
                return;
            }
            _structureNameText.text = targetBuilding.BuildingData.dataName;


        }
    }
}