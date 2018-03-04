using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapGenetaroion.Dungeon
{
    public interface IGenerationPhase
    {
        BaseDungeonGenerator Generator { get; set; }
        List<IRoom> RoomList { get; set; }
        Vector2Int DungeonSize { get; set; }
        bool IsDone { get; }
        bool Pause { get; }
        void Initialize();
        IEnumerator Generate();
    }
}