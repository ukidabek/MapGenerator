using MapGenetaroion.BaseGenerator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapGenetaroion.DungeonGenerator.V2
{
    public abstract class BaseDungeonGenerationPhase : MonoBehaviour, IGenerationPhase
    {
        [SerializeField] protected bool _isDone = false;
        public bool IsDone { get { return _isDone; } }

        public bool Pause { get { return true; } }

        public abstract IEnumerator Generate(LevelGenerator generator, object[] generationData);
    }
}