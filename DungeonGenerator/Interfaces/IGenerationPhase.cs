using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapGenetaroion.Dungeon
{
    public interface IGenerationPhase
    {
        List<IRoom> RoomList { get; set; }
        Vector2Int DungeonSize { get; set; }
        bool IsDone { get; }
        void Initialize();
        IEnumerator Generate();
    }
}