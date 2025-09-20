using Project_Train.Combat.TrainSystem;
using UnityEngine;

namespace Project_Train.Combat.WaveSystem
{
    [System.Serializable]
    public struct WaveTrainData
    {
        public float startDelay;
        public TrainArraySO trainArraySO;
	}

	[CreateAssetMenu(fileName = "WaveSO", menuName = "SO/WaveSO")]
    public class WaveSO : ScriptableObject
    {
        [field: SerializeField] public WaveTrainData[] waveTrains;

        [field: SerializeField] public float EndDelay { get; private set; } = 1f;
	}
}
