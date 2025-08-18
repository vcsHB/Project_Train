using Core.Attribute;
using UnityEngine;
namespace Project_Train.DataManage.CoreDataBaseSystem
{

    public class DataSO : ScriptableObject
    {
        [field: SerializeField, ReadOnly] public int Id { get; private set; }
        public string dataName;
        [TextArea]
        public string dataDescription;

#if UNITY_EDITOR
        internal void SetId(int newId)
        {
            Id = newId;
        }

#endif
    }
}