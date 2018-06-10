using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapGenetaroion.BaseGenerator
{
    public interface IGenerationPhase
    {
        //BaseDungeonGenerator Generator { get; set; }
        bool IsDone { get; }
        bool Pause { get; }
        IEnumerator Generate(BaseDungeonGenerator generator);
    }
}