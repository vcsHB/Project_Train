using System;
using Project_Train.Core.Attribute;
using UnityEngine;
namespace Project_Train.DataManage.CoreDataBaseSystem
{

    [CreateAssetMenu(menuName = "SO/Database/StandardData")]
    public class DataSO : ScriptableObject
    {
        [field: SerializeField, ReadOnly] public int Id { get; private set; }
        public Sprite iconSprite;
        public string dataName;

        [SerializeField] protected SerializableDetailDictionary details;
        public SerializableDetailDictionary Details => details;

        [TextArea] public string dataDescription;

        public T GetDetail<T>(DataDetailType key) where T : DataDetailSO
        {
            if (details.TryGetValue(key, out DataDetailSO detailSO))
            {
                if (detailSO is T data)
                {
                    return data;
                }

            }
            return null;
        }

        


#if UNITY_EDITOR
        internal void SetId(int newId) => Id = newId;


#endif
    }

}