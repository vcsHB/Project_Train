using UnityEngine;
namespace Project_Train.Combat.TowerSystem
{
    [System.Serializable]
    public struct UpgradeData
    {
        public float upgradeCost;
        // TODO : Resource System 
        public bool isVisualChangeUpgrade; // 
        // LATER DEV : Visual Mesh? Prefab? because  not one Material
    }

    [System.Serializable]
    public class TowerUpgradeList
    {
        public UpgradeData[] upgradeDatas;
        

    }
}