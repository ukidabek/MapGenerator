using UnityEngine;
using UnityEditor;

using System.Collections;
using System.Collections.Generic;

namespace MapGenetaroion.Dungeon
{
    [CustomEditor(typeof(BaseDungeonGenerator), true)]
    public class BaseDungeonGeneratorEditor : Editor
    {
        private BaseDungeonGenerator generator = null;

        private Texture _pause = null;
        private Texture _play = null;
        private Texture _stop = null;

        private EditorGUIStack<bool> enableGUIStack = new EditorGUIStack<bool>();

        private void OnEnable()
        {
            generator = target as BaseDungeonGenerator;

            _pause = Resources.Load("pause", typeof(Texture)) as Texture;
            _play = Resources.Load("play", typeof(Texture)) as Texture;
            _stop = Resources.Load("stop", typeof(Texture)) as Texture;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            bool isEnabled = GUI.enabled;
            enableGUIStack.SetValue(ref isEnabled, Application.isPlaying);
            GUI.enabled = isEnabled;
            { 
                EditorGUILayout.BeginHorizontal();
                {
                    if(generator.State == GenerationState.Generation)
                    { 
                        if (GUILayout.Button(_stop))
                        {
                            generator.CancelGeneration();
                        }
                    }
                    else
                    {
                        if(GUILayout.Button(_play))
                        {
                            if(generator.State == GenerationState.Pause)
                                generator.ResumeGeneration();
                            else
                                generator.StartGeneration();
                        }
                    }

                    isEnabled = GUI.enabled;
                    enableGUIStack.SetValue(ref isEnabled, generator.State == GenerationState.Generation);
                    GUI.enabled = isEnabled;
                    if (GUILayout.Button(_pause))
                    {
                        generator.CancelGeneration();
                    }
                    enableGUIStack.RevertValue(ref isEnabled);
                    GUI.enabled = isEnabled;
                }
                EditorGUILayout.EndHorizontal();
            }
            enableGUIStack.RevertValue(ref isEnabled);
            GUI.enabled = isEnabled;
        }
    }
}