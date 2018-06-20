using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

namespace MapGenetaroion.DungeonGenerator
{
    public class DungeonMetadata : MonoBehaviour, ObjectsPlacerPhase.IObjectPlacerPhaseParrentList
    {
        public Layout LayoutData = null;
        public RoomInfo StartRoom = null;
        public RoomInfo EndRoom = null;
        public List<RoomInfo> RoomList = new List<RoomInfo>();

        public List<Transform> Parents
        {
            get
            {
                List<Transform> list = new List<Transform>();

                for (int i = 1; i < RoomList.Count - 1; i++)
                {
                    list.Add(RoomList[i].RoomObject.transform);
                }

                return list;
            }
        }

        public class RoomInfo
        {
            public Vector2 Position = new Vector2();
            public List<RoomInfo> ConnectedRooms = new List<RoomInfo>();
            public GameObject RoomObject = null;

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