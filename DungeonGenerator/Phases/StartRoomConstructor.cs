using UnityEngine;

using System.Collections;
using System.Collections.Generic;


using MapGenetaroion.BaseGenerator;

namespace MapGenetaroion.DungeonGenerator.Beta
{
    public class StartRoomConstructor : MonoBehaviour, IGenerationPhase
    {
        public BaseDungeonGenerator Generator { get; set; }

        //public List<IRoomInfo> RoomList { get; set; }
        private Vector2Int DungeonSize { get; set; }

        [SerializeField] private bool _isDone = false;
        public bool IsDone { get { return _isDone; } }

        [SerializeField] private bool _pause = true;
        public bool Pause { get { return _pause; } }

        [SerializeField]
        private List<GameObject> _startRoomsList = new List<GameObject>();

        private Vector2 FindStartPosition()
        {
            List<Vector2> startPositions = new List<Vector2>();
            for (int j = 0; j < DungeonSize.x; j++)
            {
                if (j != 0 && j != DungeonSize.x - 1)
                {
                    startPositions.Add(new Vector2(j, 0));
                    startPositions.Add(new Vector2(j, DungeonSize.y - 1));
                    continue;
                }

                for (int i = 0; i < DungeonSize.x; i++)
                {
                    startPositions.Add(new Vector2(j, i));
                }
            }

            return startPositions[Random.Range(0, startPositions.Count)];
        }

        public IEnumerator Generate()
        {
            DungeonGenerator generator = Generator as DungeonGenerator;

            generator.startPosition = FindStartPosition();
            Direction direction = DirectionHandler.GetDirection();
            DirectionHandler.CheckDirection(ref direction, generator.startPosition, DungeonSize);
            generator.GetRoomInfo(generator.startPosition);
            DungeonSize = generator.DungeonSize;

            generator.StartRoom = Instantiate(_startRoomsList[0]).GetComponent<DungeonRoom>();
            generator.StartRoom.RoomInfo = generator.RoomList[0] as DungeonRoomInfo;
            generator.StartRoom.ApplyPosition(25f);

            yield return new PauseYield(Generator);

            _isDone = true;
        }

        public void Initialize()
        {
            var generator = Generator as DungeonGenerator;
            generator.RoomList.Clear();
            generator.RoomList = new List<IRoomInfo>();
            DungeonSize = generator.DungeonSize;

            _isDone = false;
        }
    }
}