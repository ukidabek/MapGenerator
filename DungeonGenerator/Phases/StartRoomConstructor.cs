using UnityEngine;

using System.Collections;
using System.Collections.Generic;


using MapGenetaroion.BaseGenerator;

namespace MapGenetaroion.DungeonGenerator
{
    public class StartRoomConstructor : MonoBehaviour, IGenerationPhase
    {
        public BaseDungeonGenerator Generator { get; set; }

        public List<IRoomInfo> RoomList { get; set; }
        public Vector2Int DungeonSize { get; set; }

        [SerializeField]
        private bool _isDone = false;
        public bool IsDone { get { return _isDone; } }

        public bool Pause { get { return true; } }

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
            RoomList.Add(Generator.GetRoomInfo(generator.startPosition));


            generator.StartRoom = Instantiate(_startRoomsList[0]).GetComponent<DungeonRoom>();
            generator.StartRoom.RoomInfo = RoomList[0] as DungeonRoomInfo;
            generator.StartRoom.ApplyPosition(25f);

            //CharacterRegister hellspawnCharacterRegister = BaseCharacterRegister.Instance as CharacterRegister;
            //hellspawnCharacterRegister.AddSpawnPoint(generator.StartRoom.transform);

            yield return new PauseYield(Generator);

            _isDone = true;
        }

        public void Initialize()
        {
            RoomList = new List<IRoomInfo>();

            _isDone = false;
        }
    }
}