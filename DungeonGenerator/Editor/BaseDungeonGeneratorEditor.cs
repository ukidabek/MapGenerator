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

        private void OnEnable()
        {
            generator = target as BaseDungeonGenerator;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if(generator.State == GenerationState.Generation)
            { 
                if (GUILayout.Button("Cancel"))
                {
                    generator.CancelGeneration();
                }
            }
            else
            {
                if(GUILayout.Button("Generate"))
                {
                    generator.StartGeneration();
                }
            }
        }
    }
}