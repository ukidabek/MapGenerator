using UnityEngine;
using UnityEditor;

using System.Collections;
using System.Collections.Generic;

using MapGenetaroion.DungeonGenerator;

namespace Hellspawn.Dungeon.Generator
{
    [CustomEditor(typeof(DungeonLayoutGenerator))]
    public class DungeonLayoutGeneratorEditor : Editor
    {
        private DungeonLayoutGenerator generator = null;

        private Texture _blank = null;
        private Texture _start = null;
        private Texture _end = null;

        private Texture[] _directionIcons = new Texture[4];

        private readonly float imageSize = 25f;

        private void OnEnable()
        {
            generator = target as DungeonLayoutGenerator;


            _blank = Resources.Load("Blank", typeof(Texture)) as Texture;
            _start = Resources.Load("Start", typeof(Texture)) as Texture;
            _end = Resources.Load("End", typeof(Texture)) as Texture;

            _directionIcons[0] = Resources.Load("Up", typeof(Texture)) as Texture;
            _directionIcons[1] = Resources.Load("Right", typeof(Texture)) as Texture;
            _directionIcons[2] = Resources.Load("Down", typeof(Texture)) as Texture;
            _directionIcons[3] = Resources.Load("Left", typeof(Texture)) as Texture;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (generator.Layout != null)
            {
                int width = (int)generator.Layout.GetLongLength(0);
                int height = (int)generator.Layout.GetLongLength(1);

                for (int j = 0; j < height; j++)
                {
                    EditorGUILayout.BeginHorizontal();
                    {
                        for (int i = 0; i < width; i++)
                        {
                            Vector2 position = new Vector2(j, i);
                            if (position == generator.StartPosition)
                            {
                                GUILayout.Label(_start, GUILayout.Width(imageSize), GUILayout.Height(imageSize));
                            }
                            else if (position == generator.LastPosition)
                            {
                                GUILayout.Label(_end, GUILayout.Width(imageSize), GUILayout.Height(imageSize));
                            }
                            else if (generator.Layout[j, i])
                            {
                                GUILayout.Label(_directionIcons[(int)generator.LayoutDirection[j, i]], GUILayout.Width(imageSize), GUILayout.Height(imageSize));
                            }
                            else
                            {
                                GUILayout.Label(_blank, GUILayout.Width(imageSize), GUILayout.Height(imageSize));
                            }
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }
        }
    }
}