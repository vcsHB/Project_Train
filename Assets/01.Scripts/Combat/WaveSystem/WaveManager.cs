using System;
using System.Collections.Generic;
using UnityEngine;

namespace  Project_Train.Combat.WaveSystem
{
    public class WaveManager : MonoSingleton<WaveManager>
    {
        public List<WaveTunnel> waveTunnelList = new();

        public int TunnelCount => waveTunnelList.Count;
        public int currentClearedTunnelCount { get; private set; } = 0;

		public Dictionary<int, Action> OnWaveStartEvents = new();
		public Dictionary<int, Action<int>> OnWaveTunnelClearEvents = new();
		public Dictionary<int, Action> OnWaveTunnelCompleteEvents = new();
		public event Action OnAllWaveTunnelEndEvent;

		private void Awake()
		{
			OnAllWaveTunnelEndEvent += () => Debug.Log("OnAllWaveTunnelEndEvent");
		}

		public void AddWaveTunnel(WaveTunnel waveTunnel)
        {
			waveTunnel.OnAllWaveEndEvent += HandleIncreaseClearedTunnelCount;
			waveTunnelList.Add(waveTunnel);
			var tunnelIndex = waveTunnelList.IndexOf(waveTunnel);
			OnWaveStartEvents.Add(tunnelIndex, null);
			OnWaveTunnelClearEvents.Add(tunnelIndex, null);
			OnWaveTunnelCompleteEvents.Add(tunnelIndex, null);

			OnWaveStartEvents[tunnelIndex] += () => Debug.Log($"OnWaveStartEvents - {tunnelIndex}");
			OnWaveTunnelClearEvents[tunnelIndex] += (int waveIndex) => Debug.Log($"OnWaveTunnelClearEvents{waveIndex} - {tunnelIndex}");
			OnWaveTunnelCompleteEvents[tunnelIndex] += () => Debug.Log($"OnWaveTunnelCompleteEvents - {tunnelIndex}");
		}

		private void HandleIncreaseClearedTunnelCount()
		{
			++currentClearedTunnelCount;

			if (waveTunnelList.Count >= currentClearedTunnelCount)
				OnAllWaveTunnelEndEvent?.Invoke();
		}
	}
}
