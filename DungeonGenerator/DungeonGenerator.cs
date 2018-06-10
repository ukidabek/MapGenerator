using UnityEngine;

using System.Collections;
using System.Collections.Generic;

using MapGenetaroion.BaseGenerator;

namespace MapGenetaroion.DungeonGenerator.Beta
{
    public class DungeonGenerator : BaseDungeonGenerator
    {
        [SerializeField, Space] private Vector2Int dungeonSize = new Vector2Int();

        [Space] public DungeonRoom StartRoom = null;
        public Vector2 startPosition = Vector3.zero;

        public List<IRoomInfo> RoomList = new List<IRoomInfo>();
        public List<List<IRoomInfo>> CorridorsList = new List<List<IRoomInfo>>();

        public Vector2Int DungeonSize { get { return dungeonSize; } }

        public IRoomInfo CreateNewRoom(Vector2 position)
        {
            return new DungeonRoomInfo(DungeonSize.x - position.x, position.y) as IRoomInfo;
        }

        public IRoomInfo CreateNewRoomForCorridor(Vector2 position)
        {
            return new DungeonRoomInfo(position.x, position.y) as IRoomInfo;
        }

        public IRoomInfo GetRoomInfo(Vector2 position)
        {
            RoomList.Add(CreateNewRoom(position));
            return RoomList[RoomList.Count - 1];
        }
    }
}