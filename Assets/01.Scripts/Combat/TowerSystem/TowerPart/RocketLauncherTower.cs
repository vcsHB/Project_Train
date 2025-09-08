using UnityEngine;

namespace Project_Train.Combat.TowerSystem
{
    public class RocketLauncherTower : Tower
    {
        [SerializeField] private float _fireCycleTerm = 0.4f;
        [SerializeField] private int _capacity = 4;
        private int _currentFireCount;
        private float _reloadTime;

        protected override void TryShoot()
        {
            if (Time.time > _reloadTime && _currentFireCount < _capacity)
            {
                _currentFireCount++;
                _head.Shoot();

            }
            else if (_currentFireCount >= _capacity)
            {
                _reloadTime = Time.time + _fireCycleTerm;
                _currentFireCount = 0;

            }
        }
    }
}