using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapGenetaroion.BaseGenerator
{
    public interface IGenerationPhase
    {
        BaseDungeonGenerator Generator { get; set; }
        //List<IRoomInfo> RoomList { get; set; }
        //Vector2Int DungeonSize { get; set; }
        bool IsDone { get; }
        bool Pause { get; }
        void Initialize();
        IEnumerator Generate();
    }
}