using Project_Train.Combat.CasterSystem.HitBody;
using UnityEngine;
using UnityEngine.Events;
namespace Project_Train.BuildSystem
{

    public class Building : MonoBehaviour
    {
        public UnityEvent OnBuildingDestroyEvent;
        public UnityEvent OnBuildCompleteEvent;
        public Health HealthCompo { get; protected set; }
        public OverloadController OverloaderCompo { get; protected set; }
        [field: SerializeField] public BuildingDataSO BuildingData { get; private set; }

        protected virtual void Awake()
        {
            HealthCompo = GetComponent<Health>();
            OverloaderCompo = GetComponent<OverloadController>();
            HealthCompo.OnDieEvent.AddListener(HandleDestroy);

        }

        public virtual void HandleBuildComplete()
        {
            OnBuildCompleteEvent?.Invoke();
        }

        public virtual void HandleDestroy()
        {
            OnBuildingDestroyEvent?.Invoke();
            BuildEventChannel.InvokeDestroyEvent(this);
            Destroy(gameObject);// TODO : Pooling?
        }

#if UNITY_EDITOR

        internal void SetBuildingData(BuildingDataSO data)
        {
            BuildingData = data;
        }

#endif


    }
}