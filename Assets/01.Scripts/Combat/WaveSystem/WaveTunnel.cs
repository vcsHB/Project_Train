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

		public event Action<float> OnWaveEndDelayCooltimeEvent;

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
			// 모든 웨이브 끝
			if (_currentWaveIndex >= waves.Length)
			{
				OnAllWaveEndEvent?.Invoke();
				_waveManager.OnWaveTunnelCompleteEvents[TunnelIndex]?.Invoke();
			}
			else
			{
				{
					yield return new WaitForSeconds(_currentWaveTrainData.startDelay);

					// 처음 소환하기 전 
					if (false == _firstSpawnComplete)
					{
						_waveManager.OnWaveStartEvents[TunnelIndex]?.Invoke();
						_firstSpawnComplete = true;
					}

					_currentWaveTrainData = waves[_currentWaveIndex].waveTrains[_currentWaveTrainIndex];
					TrainSpawner.Spawn(_currentWaveTrainData.trainArraySO);
					_currentWaveTrainIndex++;
				}

				// 웨이브 끝
				if (_currentWaveTrainIndex >= waves[_currentWaveIndex].waveTrains.Length)
				{
					float timer = 0;
					float endDelay = waves[_currentWaveIndex].EndDelay;
					while (timer < endDelay)
					{
						OnWaveEndDelayCooltimeEvent?.Invoke(timer);
						timer += Time.deltaTime;
						yield return null;
					}

					yield return new WaitForSeconds(endDelay);

					_currentWaveTrainIndex = 0;
					_currentWaveIndex++;
				}
			}
		}
	}
}
