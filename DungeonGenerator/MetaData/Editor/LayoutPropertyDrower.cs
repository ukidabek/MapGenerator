using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

namespace MapGenetaroion.DungeonGenerator
{
    [CustomPropertyDrawer(typeof(Layout))]
    public class LayoutPropertyDrower : PropertyDrawer
    {
        private Layout layout = null;
        private FieldInfo layoutField = null;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            layoutField = property.serializedObject.targetObject.GetType().GetField(property.propertyPath);
            layout = layoutField.GetValue(property.serializedObject.targetObject) as Layout;

            return (layout.RowsCount + 1) * EditorGUIUtility.singleLineHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            {
                Rect rect = position;
                Rect labelRect = new Rect(position.position, new Vector2(position.width / 2, EditorGUIUtility.singleLineHeight));
                Vector2Int size = new Vector2Int(layout.RowsCount, layout.ColumnsCount);
                Rect sizeRect = new Rect(new Vector2(labelRect.x + (position.width / 2), labelRect.y), new Vector2(position.width / 2, EditorGUIUtility.singleLineHeight));

                GUI.Label(labelRect, "Layout");
                size = EditorGUI.Vector2IntField(sizeRect, string.Empty, size);

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

                if(size.x != layout.RowsCount || size.y != layout.ColumnsCount)
                {
                    layoutField.SetValue(property.serializedObject.targetObject, new Layout(size));
                }
            }
            EditorGUI.EndProperty();
        }
    }
}