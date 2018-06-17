using MapGenetaroion.BaseGenerator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapGenetaroion.DungeonGenerator
{
    public class LayoutInitialization : MonoBehaviour, IGenerationInitalization
    {
        public void Initialize(LevelGenerator generator, object[] generationData)
        {
            DungeonMetadata metadata = null;
            GenerationSettings settings = null;
            for (int i = 0; i < generationData.Length; i++)
            {
                if (generationData[i] is DungeonMetadata)
                    metadata = generationData[i] as DungeonMetadata;
                if(generationData[i] is GenerationSettings)
                    settings = generationData[i] as GenerationSettings;
            }

            metadata.LayoutData = new Layout(settings.Size);
        }
    }
}