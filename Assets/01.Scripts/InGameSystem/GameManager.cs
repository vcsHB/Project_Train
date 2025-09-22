using Project_Train.UIManage.InGameSceneUI.GameOverUI;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Project_Train.InGameSystem
{

    public class GameManager : MonoBehaviour
    {
        [SerializeField] private ViewAnchorController _viewAnchorController;
        [SerializeField] private GameOverPanel _gameOverPanel;
        [SerializeField] private string _titleSceneName = "TitleScene";

        public void HandleGameOver()
        {
            //_viewAnchorController.
            _gameOverPanel.Open();
        }

        public void MoveToTitleScene()
        {
            SceneManager.LoadScene(_titleSceneName);
        }
    }
}