using UnityEngine;
namespace Project_Train.Combat.CasterSystem
{

    public class MendCaster : MonoBehaviour, ICastable
    {
        [SerializeField] private float _mendAmount;
        public void Cast(Collider target)
        {
            if (target.TryGetComponent(out IHealable mendTarget))
            {
                mendTarget.Restore(_mendAmount);
            }
        }

        public void HandleSetData(CasterData data)
        {

        }
    }
}