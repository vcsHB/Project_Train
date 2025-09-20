using System.Collections.Generic;
using Project_Train.Combat.WaveSystem;
using UnityEngine;

namespace Project_Train.UIManage.InGameSceneUI
{

    public class WavePanel : MonoBehaviour
    {
        [SerializeField] private WaveManager _waveManager;
        [SerializeField] private Transform _contentTrm;
        [SerializeField] private WaveTunnelInfoPanel _tunnelInfoPrefab;
        private WaveTunnelInfoPanel[] _infoPanels;

        //TODO : WaveManager Action Subscribe   
        private void Start()
        {
            // 

        }

        private void Initialize()
        {
            _waveManager = WaveManager.Instance;
            List<WaveTunnel> tunnelList = new(); // WaveManager Reference;
            _infoPanels = new WaveTunnelInfoPanel[tunnelList.Count];
            for (int i = 0; i < _infoPanels.Length; i++)
            {
                WaveTunnelInfoPanel infoPanel = Instantiate(_tunnelInfoPrefab, _contentTrm);
                _infoPanels[i] = infoPanel;

                //_waveManager.OnAllWaveTunnelEndEvent

            }
        }

    }
}