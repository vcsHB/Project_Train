using Project_Train.Combat.CasterSystem.HitBody;
using Project_Train.ResourceSystem;
using UnityEngine;
namespace Project_Train.TrainSystem
{

    public class RewardDropper : MonoBehaviour
    {
        [Header("Essential Settings")]
        [SerializeField] private Health _owner;
        [Space(10f)]

        [Header("Reward Settings")]
        [SerializeField] private ResourceManagerSO _resourceManager;
        [SerializeField] private RewardData[] _rewardDatas;


        private void Awake()
        {
            if (_owner == null)
            {
                Debug.LogError("[!] Reward Dropper's Owner is not Binded");
                return;
            }
            _owner.OnDieEvent.AddListener(Drop);
        }

        public void Drop()
        {
            for (int i = 0; i < _rewardDatas.Length; i++)
            {
                _resourceManager.Add(_rewardDatas[i].resourceType, _rewardDatas[i].amount);

            }
        }
    }
}