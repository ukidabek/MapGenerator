using UnityEngine;

using System.Collections;
using System.Collections.Generic;

namespace MapGenetaroion.Dungeon
{
    public class PauseYeald : CustomYieldInstruction
    {
        private BaseDungeonGenerator _generator = null;

        public PauseYeald(BaseDungeonGenerator generator)
        {
            this._generator = generator;
        }

        public override bool keepWaiting { get { return _generator.State == GenerationState.Pause; } }
    }
}