using UnityEngine;
namespace Project_Train.DataManage.CoreDataBaseSystem
{
    [CreateAssetMenu(menuName = "SO/DataGroupSO")]
    public class DataGroupSO : ScriptableObject
    {
        public DataSO[] datas;

#if UNITY_EDITOR

        private void OnValidate()
        {
            if (datas == null) return;
            for (int i = 0; i < datas.Length; i++)
            {
                datas[i].SetId(i);
            }
        }

#endif
    }
}