using Project_Train.Combat.TrainSystem;
using Project_Train.RailSystem;
using System;
using System.Collections;
using UnityEngine;

namespace  Project_Train.Combat.WaveSystem
{
    public class WaveTunnel : MonoBehaviour
    {
		[field: SerializeField] public Rail StartRail { get; private set; }
		public WaveSO[] waves;

		private int _currentWaveIndex = 0;
		private int _currentWaveTrainIndex = 0;
		public int TunnelIndex { get; private set; }

		private bool _firstSpawnComplete = false;
		private WaveTrainData _currentWaveTrainData;
		public TrainSpawner TrainSpawner { get; private set; }
		public event Action OnAllWaveEndEvent;

		private WaveManager _waveManager;

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

			_waveManager = WaveManager.Instance;
			_waveManager.AddWaveTunnel(this);
			TunnelIndex = _waveManager.waveTunnelList.IndexOf(this);
		}

		private void Start()
		{
			StartCoroutine(CoroutineSpawnNextTrainArray());
		}

		private void HandleGenerateNextCar()
		{
			_waveManager.OnWaveTunnelClearEvents[TunnelIndex]?.Invoke(_currentWaveIndex);
			StartCoroutine(CoroutineSpawnNextTrainArray());
		}

		private IEnumerator CoroutineSpawnNextTrainArray()
		{
			if (_currentWaveIndex >= waves.Length)
			{
				OnAllWaveEndEvent?.Invoke();
				_waveManager.OnWaveTunnelCompleteEvents[TunnelIndex]?.Invoke();
			}
			else
			{
				_currentWaveTrainData = waves[_currentWaveIndex].waveTrains[_currentWaveTrainIndex];
				yield return new WaitForSeconds(_currentWaveTrainData.startDelay);

				TrainSpawner.Spawn(_currentWaveTrainData.trainArraySO);

				if (false == _firstSpawnComplete)
				{
					_waveManager.OnWaveStartEvents[TunnelIndex]?.Invoke();
					_firstSpawnComplete = true;
				}

				_currentWaveTrainIndex++;

				if (_currentWaveTrainIndex >= waves[_currentWaveIndex].waveTrains.Length)
				{
					yield return new WaitForSeconds(waves[_currentWaveIndex].EndDelay);

					_currentWaveTrainIndex = 0;
					_currentWaveIndex++;
				}
			}
		}
	}
}
