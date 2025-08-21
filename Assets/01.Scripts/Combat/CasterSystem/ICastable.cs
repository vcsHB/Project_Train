using UnityEngine;

namespace Project_Train.Combat.CasterSystem
{
    public interface ICastable
    {
        public void HandleSetData(CasterData data);
        public void Cast(Collider target);
    }
}