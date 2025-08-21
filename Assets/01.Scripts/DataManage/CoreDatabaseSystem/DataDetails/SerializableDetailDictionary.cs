using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Project_Train.DataManage.CoreDataBaseSystem
{
    [Serializable]
    public class SerializableDetailDictionary
    {
        [Serializable]
        public class Entry
        {
            public DataDetailType key;
            public DataDetailSO value;
            public bool foldout; // use for Toggle in Inspector
        }

        [SerializeField] private List<Entry> entries = new List<Entry>();
        public List<Entry> Entries => entries;

        public bool TryGetValue(DataDetailType key, out DataDetailSO so)
        {
            foreach (var e in entries)
            {
                if (e.key == key)
                {
                    so = e.value;
                    return true;
                }
            }
            so = null;
            return false;
        }

#if UNITY_EDITOR

        public void AddDetail(DataDetailType type, ScriptableObject parent)
        {
            if (entries.Exists(e => e.key == type)) return;

            var soType = GetDetailTypeClass(type);
            if (soType == null) return;

            var instance = ScriptableObject.CreateInstance(soType) as DataDetailSO;
            instance.name = type.ToString() + "Detail";

            // Add for SubAsset (parent => DataSO)
            AssetDatabase.AddObjectToAsset(instance, parent);
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(parent));

            entries.Add(new Entry { key = type, value = instance, foldout = true });
        }

        private Type GetDetailTypeClass(DataDetailType type)
        {
            switch (type)
            {
                case DataDetailType.Subject: return typeof(SubjectDetailSO);
                case DataDetailType.Function: return typeof(FunctionDetailSO);
                case DataDetailType.Build: return typeof(BuildDetailSO);
            }
            return null;
        }
#endif
    }

}
