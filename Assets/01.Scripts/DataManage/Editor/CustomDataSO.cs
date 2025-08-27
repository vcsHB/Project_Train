using UnityEditor;
using UnityEngine;
namespace Project_Train.DataManage.CoreDataBaseSystem
{
    [CustomEditor(typeof(DataSO), true)]
    public class DataSOEditor : Editor
    {
        private DataDetailType newType;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            DrawPropertiesExcluding(serializedObject, "details");

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Details", EditorStyles.boldLabel);

            var dataSO = (DataSO)target;
            var dictProp = serializedObject.FindProperty("details").FindPropertyRelative("entries");

            for (int i = 0; i < dictProp.arraySize; i++)
            {
                var entry = dictProp.GetArrayElementAtIndex(i);
                var keyProp = entry.FindPropertyRelative("key");
                var valProp = entry.FindPropertyRelative("value");
                var foldoutProp = entry.FindPropertyRelative("foldout");

                var keyName = keyProp.enumDisplayNames[keyProp.enumValueIndex];
                foldoutProp.boolValue = EditorGUILayout.Foldout(foldoutProp.boolValue, keyName, true);

                if (foldoutProp.boolValue)
                {
                    EditorGUI.indentLevel++;

                    // Draw Reference Field.... (object field)
                    EditorGUILayout.PropertyField(valProp, new GUIContent("Reference"), true);

                    // Draw DetailSO inner Fields
                    if (valProp.objectReferenceValue != null)
                    {
                        var detailSO = valProp.objectReferenceValue as ScriptableObject;
                        var soSerialized = new SerializedObject(detailSO);

                        soSerialized.Update();
                        var prop = soSerialized.GetIterator();
                        prop.NextVisible(true);

                        EditorGUI.indentLevel++;
                        while (prop.NextVisible(false))
                        {
                            if (prop.name == "m_Script") continue; // HIDE ScriptField (I Hate This!!)
                            EditorGUILayout.PropertyField(prop, true);
                        }
                        EditorGUI.indentLevel--;

                        soSerialized.ApplyModifiedProperties();
                    }

                    EditorGUI.indentLevel--;
                }
            }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Add Detail", EditorStyles.boldLabel);

            newType = (DataDetailType)EditorGUILayout.EnumPopup("Detail Type", newType);
            if (GUILayout.Button("Add Detail"))
            {
                dataSO.Details.AddDetail(newType, dataSO);
                EditorUtility.SetDirty(dataSO);
                AssetDatabase.SaveAssets();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}