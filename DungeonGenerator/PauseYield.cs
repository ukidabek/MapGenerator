using UnityEngine;

using System.Collections;
using System.Collections.Generic;

namespace MapGenetaroion.Dungeon
{
    public class PauseYield : CustomYieldInstruction
    {
        private BaseDungeonGenerator _generator = null;

        public PauseYield(BaseDungeonGenerator generator)
        {
            this._generator = generator;
        }

        public override bool keepWaiting { get { return _generator.State == GenerationState.Pause; } }
    }
}