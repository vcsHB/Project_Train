using Project_Train.DataManage.CoreDataBaseSystem;
using UnityEngine;
namespace Project_Train.UIManage.InGameSceneUI.DataBaseUIManage
{

    public class BuildDetailSlot : DataDetailSlot
    {
        [SerializeField] private Transform _requireResourceDisplayerSlotGroupTrm;
        private RequireResourceContent[] _slots;


        protected override void Awake()
        {
            base.Awake();
            _slots = _requireResourceDisplayerSlotGroupTrm.GetComponentsInChildren<RequireResourceContent>();

        }
        public override void SetData(DataDetailSO detail)
        {
            if (detail is BuildDetailSO buildDetail)
            {
                for (int i = 0; i < _slots.Length; i++)
                {
                    bool isSlotEnable = i < buildDetail.RequireResource.Count;
                    _slots[i].SetEnable(isSlotEnable);
                }
                int index = 0;
                foreach (var pair in buildDetail.RequireResource)
                {
                    _slots[index].SetResourceData(pair.Key, pair.Value);
                    ++index;
                }

            }
            else
            {
                Debug.LogError($"Unexpected Detail data : detail is {detail.DetailType}");
            }
        }
    }
}