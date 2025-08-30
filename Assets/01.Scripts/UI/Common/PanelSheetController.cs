using System;
using System.Collections.Generic;
using Project_Train.Core.Input;
using UnityEngine;
namespace Project_Train.UIManage
{
    public class PanelSheetController : MonoBehaviour
    {
        [SerializeField] private FadePanel _initPanel;
        [SerializeField] private FadePanel[] _panels;
        private Stack<FadePanel> _enabledPanelOrder = new();

        private readonly string _panelCancelInputKey = "EscInputKey";

        private void Awake()
        {
            for (int i = 0; i < _panels.Length; i++)
            {
                _panels[i].OnPanelEnabledEvent += HandlePanelOpened;
                _panels[i].OnPanelDisabledEvent += HandlePanelClosed;
            }
            _enabledPanelOrder.Push(_initPanel);
            _initPanel.Open();

            InputReader.AddListener()
        }

        private void CLoseCurrentPanel()
        {
            if (_enabledPanelOrder.Count > 1)
            {
                FadePanel currentPanel =_enabledPanelOrder.Pop();
                currentPanel.Close();
                _enabledPanelOrder.Peek().Open();

            }
        }

        private void HandlePanelClosed(FadePanel panel)
        {
            if (_enabledPanelOrder.TryPeek(out FadePanel currentPanel))
            {
                if (currentPanel != panel) return;

                _enabledPanelOrder.Pop();
                _enabledPanelOrder.Peek().Open();

            }

        }

        private void HandlePanelOpened(FadePanel panel)
        {
            _enabledPanelOrder.Peek().Close();
            _enabledPanelOrder.Push(panel);
        }

    }
}