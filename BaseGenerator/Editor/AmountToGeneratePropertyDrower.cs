using UnityEngine;
using UnityEditor;

using System.Collections;
using System.Collections.Generic;

namespace MapGenetaroion
{
    [CustomPropertyDrawer(typeof(AmountToGenerate))]
    public class AmountToGeneratePropertyDrower : PropertyDrawer
    {
        private SerializedProperty typeProperty = null;
        private AmountToGenerate.Type _type;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            typeProperty = property.FindPropertyRelative("_type");
            _type = (AmountToGenerate.Type)typeProperty.intValue;

            switch (_type)
            {
                case AmountToGenerate.Type.Static:
                    return EditorGUIUtility.singleLineHeight * 3;
                case AmountToGenerate.Type.Random:
                    return EditorGUIUtility.singleLineHeight * 4;
            }

            return base.GetPropertyHeight(property, label);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Rect rect = position;
            rect.height = EditorGUIUtility.singleLineHeight;
            GUI.Label(rect, "Amount");
            rect.y += EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(rect, typeProperty, new GUIContent("Type"));

            switch (_type)
            {
                case AmountToGenerate.Type.Static:
                    rect.y += EditorGUIUtility.singleLineHeight;
                    EditorGUI.PropertyField(rect, property.FindPropertyRelative("_count"), new GUIContent("Constant"));
                    break;
                case AmountToGenerate.Type.Random:
                    rect.y += EditorGUIUtility.singleLineHeight;
                    EditorGUI.PropertyField(rect, property.FindPropertyRelative("_minCount"), new GUIContent("Min"));
                    rect.y += EditorGUIUtility.singleLineHeight;
                    EditorGUI.PropertyField(rect, property.FindPropertyRelative("_maxCount"), new GUIContent("Max"));
                    break;
            }
        }
    }
}