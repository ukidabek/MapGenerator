using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapGenetaroion.BaseGenerator
{
    public interface IGenerationPhase
    {
        bool IsDone { get; set; }
        bool Pause { get; }
        IEnumerator Generate(LevelGenerator generator, object[] generationData);
    }
}