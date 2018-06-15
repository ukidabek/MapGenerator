using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

namespace MapGenetaroion.DungeonGenerator.V2
{
    public class DungeonMetadata : MonoBehaviour
    {
        public Layout LayoutData = null;
        public RoomInfo StartRoom = null;

        public class RoomInfo
        {
            public Vector2 Position = new Vector2();
            public List<RoomInfo> ConnectedRooms = new List<RoomInfo>();

            public enum RoomType
            {
                Start,
                Normal,
                End,
                Corridor
            }

            public RoomType Type = RoomType.Normal;

            public RoomInfo(Vector2 position) : this(position, RoomType.Normal) {}

            public RoomInfo(Vector2 position, RoomType type)
            {
                Position = position;
                Type = type;
            }
        }
    }
}