using System;
using Project_Train.Core.Attribute;
using UnityEngine;
namespace Project_Train.DataManage.CoreDataBaseSystem
{

    [CreateAssetMenu(menuName = "SO/Data/StandardData")]
    public class DataSO : ScriptableObject
    {
        [field: SerializeField, ReadOnly] public int Id { get; private set; }
        public Sprite iconSprite;
        public string dataName;

        [SerializeField] private SerializableDetailDictionary details;

        [TextArea] public string dataDescription;


#if UNITY_EDITOR
        internal void SetId(int newId) => Id = newId;


        public SerializableDetailDictionary Details => details;
#endif
    }

}