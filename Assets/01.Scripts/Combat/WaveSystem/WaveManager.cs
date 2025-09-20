using Project_Train.Combat.TrainSystem;
using Project_Train.RailSystem;
using System.Collections;
using UnityEngine;

namespace  Project_Train.Combat.WaveSystem
{
    public class WaveManager : MonoSingleton<WaveManager>
    {
		[field: SerializeField] public Rail StartRail { get; private set; }
		public WaveSO[] waves;
		private int _currentWaveIndex = 0;
		private int _currentWaveTrainIndex = 0;
		private WaveTrainData _currentWaveTrainData;

		public TrainSpawner TrainSpawner { get; private set; }

		private void Awake()
		{
			if (null == StartRail)
			{
				Debug.LogError("Start rail is null.");
				return;
			}

			TrainSpawner = new TrainSpawner();
			TrainSpawner.Initialize(this);
			TrainSpawner.startRail = StartRail;
			TrainSpawner.OnTrainArraySpawnComplete += HandleGenerateNextCar;
		}

		private void Start()
		{
			HandleGenerateNextCar();
		}

		private void HandleGenerateNextCar()
		{
			StartCoroutine(CoroutineSpawnNextTrainArray());
		}

		private IEnumerator CoroutineSpawnNextTrainArray()
		{
			if (_currentWaveIndex >= waves.Length)
			{
				Debug.Log("End.");
			}
			else
			{
				_currentWaveTrainData = waves[_currentWaveIndex].waveTrains[_currentWaveTrainIndex];
				yield return new WaitForSeconds(_currentWaveTrainData.startDelay);

				TrainSpawner.Spawn(_currentWaveTrainData.trainArraySO);
				_currentWaveTrainIndex++;

				if (_currentWaveTrainIndex >= waves[_currentWaveIndex].waveTrains.Length)
				{
					_currentWaveTrainIndex = 0;
					_currentWaveIndex++;
				}
			}
		}
	}
}
