using UnityEngine;
namespace Project_Train.Combat.TowerSystem
{

    public class CannonTower : Tower
    {
        protected override void TryShoot()
        {
            _head.Shoot();
        }
    }
}