using UnityEngine;
namespace Project_Train.Combat.TowerSystem
{

    public class TowerPart : MonoBehaviour
    {
        
        [SerializeField] protected TowerUpgradeList _upgradeList;
        [SerializeField] protected PartVisual _partVisual;
        [SerializeField] private int _level = 0; // Init Level = 0
        [SerializeField] protected MeshRenderer _visualRenderer;


        public void Upgrade()
        {
            _level++;
            // CheckResource from outside
            //
            //        SetPartModel();
        }

        public void SetPartModel()
        {
            UpgradeData data = _upgradeList.upgradeDatas[_level];
            
            // Later DEV
            //_visualRenderer.
        }

    }
}