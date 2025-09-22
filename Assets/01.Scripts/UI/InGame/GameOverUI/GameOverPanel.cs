using UnityEngine;
namespace Project_Train.UIManage.InGameSceneUI.GameOverUI
{

    public class GameOverPanel : FadePanel
    {
        [SerializeField] private GameOverInfoPanel _infoPanel;


        public override void Open()
        {
            base.Open();
            _infoPanel.Open();
        }
    }
}