using Project_Train.UIManage;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Project_Train.TitleScene
{

    public class TitleSceneManager : MonoBehaviour
    {
        [SerializeField] private FadePanel _initPanel;
        [SerializeField] private string _inGameSceneName;


        private void Awake()
        {
            _initPanel.Close();
        }
        
        public void MoveToInGameScene()
        {
            SceneManager.LoadScene(_inGameSceneName);
        }
        public void HandleQuitGame()
        {
            Application.Quit();
        }
    }
}