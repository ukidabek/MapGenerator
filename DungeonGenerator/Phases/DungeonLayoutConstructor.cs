using UnityEngine;

using System.Collections;
using System.Collections.Generic;

using MapGenetaroion.BaseGenerator;

namespace MapGenetaroion.DungeonGenerator.Beta
{
    public class DungeonLayoutConstructor : MonoBehaviour, IGenerationPhase
    {
        [SerializeField] private List<GameObject> _rooms = new List<GameObject>();

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
            List<List<IRoomInfo>> dungeon = new List<List<IRoomInfo>>();
            dungeon.Add(dungeonGenerator.RoomList);
            dungeon.AddRange(dungeonGenerator.CorridorsList);
            for (int i = 0; i < dungeon.Count; i++)
            {
                List<IRoomInfo> RoomList = dungeon[i];
                for (int j = i == 0 ? 1 : 0; j < RoomList.Count; j++)
                {
                    DungeonRoomInfo info = RoomList[j] as DungeonRoomInfo;

                    DungeonRoom dungeonRoom = null;
                    dungeonRoom = Instantiate(GetRoomPrefab()).GetComponent<DungeonRoom>();
                    dungeonRoom.RoomInfo = info;
                    dungeonRoom.gameObject.name = j.ToString();
                    dungeonRoom.ApplyPosition(_roomSize);
                    dungeonRoom.transform.SetParent(this.transform);
                    _dungeonRooms.Add(dungeonRoom);

                    info.RoomObject = dungeonRoom.gameObject;

                    dungeonRoom.SetWalls(
                        j > 0 ? RoomList[j - 1] as DungeonRoomInfo : null,
                        j < RoomList.Count - 1 ? RoomList[j + 1] as DungeonRoomInfo : null);

                    yield return new PauseYield(Generator);
                }

            }

            _dungeonRooms.Add(dungeonGenerator.StartRoom);
            dungeonGenerator.StartRoom.SetWalls(null, dungeon[0][1] as DungeonRoomInfo);

            _isDone = true;
        }
    }
}