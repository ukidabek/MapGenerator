using UnityEngine;
using UnityEditor;

using System.Collections;
using System.Collections.Generic;

namespace MapGenetaroion.Dungeon
{
    [CustomEditor(typeof(DungeonGenerator), true)]
    public class DungeonGeneratorEditor : Editor
    {
        private DungeonGenerator generator = null;

        private void OnEnable()
        {
            generator = target as DungeonGenerator;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if(generator.State != GenerationState.Finished)
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