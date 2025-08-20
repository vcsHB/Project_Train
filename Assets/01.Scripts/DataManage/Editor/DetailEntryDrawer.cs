
using UnityEditor;
using UnityEngine;
namespace Project_Train.DataManage.CoreDataBaseSystem
{

    [CustomPropertyDrawer(typeof(SerializableDetailDictionary.Entry))]
    public class DetailEntryDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var keyProp = property.FindPropertyRelative("key");
            var valueProp = property.FindPropertyRelative("value");

            var rect = new Rect(position.x, position.y, position.width * 0.4f, position.height);
            EditorGUI.PropertyField(rect, keyProp, GUIContent.none);

            rect.x += rect.width + 5;
            rect.width = position.width * 0.6f - 5;

            EditorGUI.PropertyField(rect, valueProp, GUIContent.none);
        }
    }
}