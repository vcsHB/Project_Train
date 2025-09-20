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

			_waveManager = WaveManager.Instance;
			_waveManager.AddWaveTunnel(this);
			TunnelIndex = _waveManager.waveTunnelList.IndexOf(this);
		}

		private void Start()
		{
			StartCoroutine(CoroutineSpawnNextTrainArray());
		}

		private IEnumerator CoroutineSpawnNextTrainArray()
		{
			for (int curWaveIndex = 0; curWaveIndex < waves.Length; ++curWaveIndex)
			{
				_currentWaveIndex = curWaveIndex;

				for (int curWaveTrainIndex = 0; curWaveTrainIndex < waves[curWaveIndex].waveTrains.Length; ++curWaveTrainIndex)
				{
					_currentWaveTrainIndex = curWaveTrainIndex;

					_currentWaveTrainData = waves[_currentWaveIndex].waveTrains[_currentWaveTrainIndex];
					yield return new WaitForSeconds(_currentWaveTrainData.startDelay);

					// First spawn
					if (false == _firstSpawnComplete)
					{
						_waveManager.OnWaveStartEvents[TunnelIndex]?.Invoke();
						_firstSpawnComplete = true;
					}

					yield return TrainSpawner.CoroutineSpawn(_currentWaveTrainData.trainArraySO);
					_waveManager.OnWaveTunnelClearEvents[TunnelIndex]?.Invoke(_currentWaveIndex);
					_currentWaveTrainIndex++;
				}

				float timer = 0;
				float endDelay = waves[_currentWaveIndex].EndDelay;
				_currentWaveIndex++;
				_currentWaveTrainIndex = 0;

				// Wave's end delay time
				while (timer < endDelay)
				{
					yield return null;
					OnWaveEndDelayCooltimeEvent?.Invoke(timer);
					timer += Time.deltaTime;
				}
			}

			OnAllWaveEndEvent?.Invoke();
			_waveManager.OnWaveTunnelCompleteEvents[TunnelIndex]?.Invoke();
		}
	}
}
