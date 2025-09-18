using Project_Train.DataManage.CoreDataBaseSystem;
using UnityEngine;
namespace Project_Train.UIManage.InGameSceneUI.BuildUI
{

    public class BuildRequireResourcePanel : MonoBehaviour
    {
        [SerializeField] private BuildRequireResourceSlot[] _resourceSlots;
        private BuildDetailSO _detailData;

        public void SetData(BuildDetailSO detailData)
        {
            _detailData = detailData;
            for (int i = 0; i < _resourceSlots.Length; i++)
            {
                bool isSlotEnable = i < detailData.RequireResource.Count;
                BuildRequireResourceSlot slot = _resourceSlots[i];
                slot.SetEnable(isSlotEnable);
                slot.Unsubscribe();
            }
            int index = 0;
            foreach (var pair in detailData.RequireResource)
            {
                _resourceSlots[index].SetResourceData(pair.Key, pair.Value);
                ++index;
            }
        }
    }
}