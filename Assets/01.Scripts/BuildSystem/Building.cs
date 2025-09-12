using Project_Train.Combat.CasterSystem.HitBody;
using UnityEngine;
namespace Project_Train.BuildSystem
{

    public class Building : MonoBehaviour
    {
        public Health HealthCompo { get; protected set; }

        protected virtual void Awake()
        {
            HealthCompo = GetComponent<Health>();

        }

        
    }
}