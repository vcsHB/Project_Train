using TMPro;
using UnityEngine;
namespace Project_Train.UIManage.InGameSceneUI.BuildUI
{

    public class TerrainStatusSlot : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _terrainStatusNameText;
        [SerializeField] private TextMeshProUGUI _terrainStatusValueText;

        public void SetTerrainStatus(string valueContent)
        {
            _terrainStatusValueText.text = valueContent;
        }
    }
}