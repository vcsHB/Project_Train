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
            Initialize();
        }

        private void Initialize()
        {
            _waveManager = WaveManager.Instance;
            List<WaveTunnel> tunnelList = _waveManager.waveTunnelList;
            _infoPanels = new WaveTunnelInfoPanel[tunnelList.Count];
            for (int i = 0; i < _infoPanels.Length; i++)
            {
                WaveTunnelInfoPanel infoPanel = Instantiate(_tunnelInfoPrefab, _contentTrm);
                infoPanel.Initialize(tunnelList[i]);
                _infoPanels[i] = infoPanel;

                //_waveManager.OnAllWaveTunnelEndEvent

            }
        }

    }
}