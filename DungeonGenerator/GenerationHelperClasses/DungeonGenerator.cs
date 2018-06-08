using UnityEngine;

using System.Collections;
using System.Collections.Generic;

using MapGenetaroion.BaseGenerator;

namespace MapGenetaroion.DungeonGenerator
{
    public class DungeonGenerator : BaseDungeonGenerator
    {
        [Space]
        public DungeonRoom StartRoom = null;
        public Vector2 startPosition = Vector3.zero;

        public override IRoomInfo GetRoomInfo(Vector2 position)
        {
            return new DungeonRoomInfo(_dungeonSize.x - position.x, position.y) as IRoomInfo;
        }
    }
}