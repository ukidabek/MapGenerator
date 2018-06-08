using UnityEngine;

using System.Collections;
using System.Collections.Generic;

namespace MapGenetaroion.DungeonGenerator
{
    public class DungeonRoom : MonoBehaviour
    {
        public DungeonRoomInfo RoomInfo = null;

        [SerializeField]
        private GameObject[] _walls = new GameObject[4];

        [SerializeField]
        private GameObject[] _passages = new GameObject[4];

        private void Awake()
        {
            for (int i = 0; i < _passages.Length; i++)
            {
                _passages[i].SetActive(false);
            }    
        }

        public void ApplyPosition(float roomSize)
        {
            Vector2 position = RoomInfo.Position;

            transform.position = new Vector3(
                (roomSize) * (position.y),
                0f,
                (roomSize) * (position.x));
        }

        private void SetWalls(DungeonRoomInfo neighboringRoom)
        {
            if (neighboringRoom != null)
            {
                if (neighboringRoom.Position.x == RoomInfo.Position.x && neighboringRoom.Position.y < RoomInfo.Position.y)
                {
                    _walls[(int)Direction.Left].SetActive(false);
                    _passages[(int)Direction.Left].SetActive(true);
                }

                if (neighboringRoom.Position.x == RoomInfo.Position.x && neighboringRoom.Position.y > RoomInfo.Position.y)
                {
                    _walls[(int)Direction.Right].SetActive(false);
                    _passages[(int)Direction.Right].SetActive(true);
                }

                if (neighboringRoom.Position.x < RoomInfo.Position.x && neighboringRoom.Position.y == RoomInfo.Position.y)
                {
                    _walls[(int)Direction.Down].SetActive(false);
                    _passages[(int)Direction.Down].SetActive(true);
                }

                if (neighboringRoom.Position.x > RoomInfo.Position.x && neighboringRoom.Position.y == RoomInfo.Position.y)
                {
                    _walls[(int)Direction.Up].SetActive(false);
                    _passages[(int)Direction.Up].SetActive(true);
                }
            }
        }

        public void SetWalls(DungeonRoomInfo previous, DungeonRoomInfo next)
        {
            if(previous != null)
                SetWalls(previous);

            if (next != null)
                SetWalls(next);
        }
    }
}