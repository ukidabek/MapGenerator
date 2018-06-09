using UnityEngine;

using System.Collections;
using System.Collections.Generic;

using MapGenetaroion.BaseGenerator;

namespace MapGenetaroion.DungeonGenerator
{
    public class DungeonLayoutConstructor : MonoBehaviour, IGenerationPhase
    {
        [SerializeField]
        private List<GameObject> _rooms = new List<GameObject>();

        //public List<IRoomInfo> RoomList { get; set; }

        public Vector2Int DungeonSize { get; set; }

        public BaseDungeonGenerator Generator { get; set; }

        [SerializeField] private List<DungeonRoom> _dungeonRooms = new List<DungeonRoom>();

        [SerializeField] private bool _isDone = false;
        public bool IsDone { get { return _isDone; } }

        [SerializeField] private float _roomSize = 25f;

        public bool Pause { get { return false; } }

        public void Initialize()
        {
            _isDone = false;
        }

        private GameObject GetRoomPrefab()
        {
            int index = Random.Range(0, _rooms.Count);
            return _rooms[index];
        }

        public IEnumerator Generate()
        {
            for (int i = 0; i < _dungeonRooms.Count; i++)
            {
                Destroy(_dungeonRooms[i].gameObject);
                yield return new PauseYield(Generator);
            }

            _dungeonRooms.Clear();

            DungeonGenerator dungeonGenerator = (Generator as DungeonGenerator);
            List<IRoomInfo> RoomList = dungeonGenerator.RoomList;
            for (int i = 1; i < RoomList.Count; i++)
            {
                DungeonRoomInfo info = RoomList[i] as DungeonRoomInfo;

                DungeonRoom dungeonRoom = null;
                dungeonRoom = Instantiate(GetRoomPrefab()).GetComponent<DungeonRoom>();
                dungeonRoom.RoomInfo = info;
                dungeonRoom.gameObject.name = i.ToString();
                dungeonRoom.ApplyPosition(_roomSize);
                dungeonRoom.transform.SetParent(this.transform);
                _dungeonRooms.Add(dungeonRoom);

                info.RoomObject = dungeonRoom.gameObject;

                dungeonRoom.SetWalls(
                    i > 0 ? RoomList[i - 1] as DungeonRoomInfo : null,
                    i < RoomList.Count - 1 ? RoomList[i + 1] as DungeonRoomInfo : null);

                yield return new PauseYield(Generator);
            }

            _dungeonRooms.Add(dungeonGenerator.StartRoom);
            dungeonGenerator.StartRoom.SetWalls(null, RoomList[1] as DungeonRoomInfo);

            _isDone = true;
        }
    }
}