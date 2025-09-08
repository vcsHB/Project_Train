using UnityEngine;
namespace Project_Train.UIManage.InGameSceneUI
{

    public class GameUIPanel : FadePanel
    {
        [field: SerializeField] public float UIHeight { get; protected set; } = 200f;
        
        
    }
}