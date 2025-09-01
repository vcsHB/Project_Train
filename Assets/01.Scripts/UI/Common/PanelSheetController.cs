using System;
using System.Collections.Generic;
using Project_Train.Core.Input;
using Project_Train.UIManage.InGameSceneUI;
using UnityEngine;
using DG.Tweening;

namespace Project_Train.UIManage
{
    public class PanelSheetController : MonoBehaviour
    {
        [SerializeField] private RectTransform _gameUIBaseRectTrm;
        [SerializeField] private float _panelExpandDuration = 0.2f;
        [Header("Essential Settings")]
        [SerializeField] private FadePanel _initPanel;
        [SerializeField] private FadePanel _outermostPanel;
        [SerializeField] private GameUIPanel[] _panels;
        private Stack<FadePanel> _enabledPanelOrder = new();

        private readonly string _panelCancelInputKey = "OnCancelEvent";

        private void Awake()
        {
            _initPanel.OnPanelEnabledEvent += HandlePanelOpened;
            _outermostPanel.OnPanelEnabledEvent += HandlePanelOpened;
            _outermostPanel.OnPanelDisabledEvent += HandlePanelClosed;

            for (int i = 0; i < _panels.Length; i++)
            {
                _panels[i].OnPanelEnabledEvent += HandlePanelOpened;
                _panels[i].OnPanelDisabledEvent += HandlePanelClosed;
            }
            _enabledPanelOrder.Push(_initPanel);
            _initPanel.Open();

            InputReader.AddListener(_panelCancelInputKey, CLoseCurrentPanel);
        }

        private void CLoseCurrentPanel()
        {
            if (_enabledPanelOrder.Count > 1)
            {
                FadePanel currentPanel = _enabledPanelOrder.Pop();
                currentPanel.Close();
                _enabledPanelOrder.Peek().Open();

            }
            else
            {
                _outermostPanel.Open();
                _enabledPanelOrder.Push(_outermostPanel);
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
            if (panel is GameUIPanel gameUIPanel)
            {
                _gameUIBaseRectTrm.DOSizeDelta(new Vector2(_gameUIBaseRectTrm.sizeDelta.x, gameUIPanel.UIHeight), _panelExpandDuration);

            }
            if (panel != _initPanel)
                _enabledPanelOrder.Push(panel);
        }

    }
}