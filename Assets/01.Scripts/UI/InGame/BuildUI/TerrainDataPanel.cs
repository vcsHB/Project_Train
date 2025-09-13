using Project_Train.TerrainSystem;
using TMPro;
using UnityEngine;
namespace Project_Train.UIManage.InGameSceneUI.BuildUI
{

    public class TerrainDataPanel : MonoBehaviour
    {

        [SerializeField] private TerrainStatusSlot _heightStatusSlot;
        [SerializeField] private TerrainStatusSlot _rangeStatusSlot;

        public void SetData(TerrainStatus status)
        {
            _heightStatusSlot.SetTerrainStatus($"+{status.height}m");
            _rangeStatusSlot.SetTerrainStatus($"+{status.rangeBenefit} Radius");

        }

    }
}