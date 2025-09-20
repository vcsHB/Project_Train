using UnityEngine;

namespace Project_Train.Combat.TrainSystem
{
    [CreateAssetMenu(fileName = "TrainArraySO", menuName = "SO/TrainArraySO")]
    public class TrainArraySO : ScriptableObject
    {
        [field:SerializeField] public CarBase[] CarPrefabs { get; private set; }

        public int Count => CarPrefabs.Length;

        public CarBase this[int index] 
            => CarPrefabs[index];
    }
}
