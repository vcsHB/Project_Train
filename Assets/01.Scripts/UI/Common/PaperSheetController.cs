using System;
using System.Collections.Generic;
using UnityEngine;
namespace Project_Train.UIManage
{
    public class PaperSheetController : MonoBehaviour
    {
        [SerializeField] private FadePanel[] _papers;
        private Stack<FadePanel> _enabledPanel;

        private void Awake()
        {
            for (int i = 0; i < _papers.Length; i++)
            {
                _papers[i].OnPanelOpenEvent.AddListener(HandlePanelOpened);
                _papers[i].OnPanelCloseEvent.AddListener(HandlePanelClosed);
            }
        }

        private void HandlePanelOpened()
        {
            
        }

        private void HandlePanelClosed()
        {
        }
    }
}