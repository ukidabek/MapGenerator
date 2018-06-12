using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MapGenetaroion.DungeonGenerator.V2
{
    [CustomPropertyDrawer(typeof(Layout))]
    public class LayoutPropertyDrower : PropertyDrawer
    {
        Layout layout = null;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            layout = property.serializedObject.targetObject.GetType().GetField(property.propertyPath).GetValue(property.serializedObject.targetObject) as Layout;

            return (layout.RowsCount + 1) * EditorGUIUtility.singleLineHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            {
                Rect rect = position;
                Rect labelRect = new Rect(position.position, new Vector2(position.width, EditorGUIUtility.singleLineHeight));
                GUI.Label(labelRect, "Layout");
                rect.y += EditorGUIUtility.singleLineHeight;
                rect.size = new Vector2(EditorGUIUtility.singleLineHeight, EditorGUIUtility.singleLineHeight);
                for (int i = layout.RowsCount - 1; i >= 0; i--)
                {
                    for (int j = 0; j < layout.ColumnsCount; j++)
                    {
                        layout[i, j] = GUI.Toggle(rect, layout[i, j], string.Empty);
                        rect.x += rect.width;
                    }
                    rect.y += EditorGUIUtility.singleLineHeight;
                    rect.x = position.x; ;
                }
            }
            EditorGUI.EndProperty();
        }
    }
}