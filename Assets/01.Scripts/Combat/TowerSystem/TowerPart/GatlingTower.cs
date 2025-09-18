using UnityEngine;
namespace Project_Train.Combat.TowerSystem
{

    public class GatlingTower : Tower
    {

        protected override void TryShoot()
        {
            _head.Shoot();
        }
    }
}