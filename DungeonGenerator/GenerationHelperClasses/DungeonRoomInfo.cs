using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

using MapGenetaroion.BaseGenerator;

namespace MapGenetaroion.DungeonGenerator
{
    [Serializable]
    public class DungeonRoomInfo : IRoomInfo
    {
        public Vector3 Position = Vector3.zero;

        public GameObject RoomObject = null;

        public DungeonRoomInfo(float x, float y)
        {
            Position.x = x;
            Position.y = y;
        }
    }
}