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

            public RoomInfo(Vector2 position)
            {
                Position = position;
            }
        }
    }
}