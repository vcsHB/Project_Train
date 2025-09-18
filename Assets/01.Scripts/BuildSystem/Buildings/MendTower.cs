using Project_Train.Combat.CasterSystem;
using UnityEngine;
using UnityEngine.Events;
namespace Project_Train.BuildSystem
{

    public class MendTower : Building
    {
        public UnityEvent OnMendEvent;
        [SerializeField] private Caster _mendCaster;
        [SerializeField] private float _mendingCooltime = 5f;

        public void Mend()
        {
            _mendCaster.Cast();
            OnMendEvent?.Invoke();
        }
    }
}