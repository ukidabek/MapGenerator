using UnityEngine;

using System.Collections;
using System.Collections.Generic;


using MapGenetaroion.BaseGenerator;
using System;

namespace MapGenetaroion.DungeonGenerator.Beta
{
    public class StartRoomConstructor : MonoBehaviour, IGenerationPhase
    {
        private Vector2Int DungeonSize { get; set; }

        [SerializeField] private bool _isDone = false;
        public bool IsDone { get { return _isDone; } }

        [SerializeField] private bool _pause = true;
        public bool Pause { get { return _pause; } }

        [SerializeField]
        private List<GameObject> _startRoomsList = new List<GameObject>();

        private Vector2 FindStartPosition(Vector2Int dungeonSize)
        {
            List<Vector2> startPositions = new List<Vector2>();
            for (int j = 0; j < dungeonSize.x; j++)
            {
                if (j != 0 && j != dungeonSize.x - 1)
                {
                    startPositions.Add(new Vector2(j, 0));
                    startPositions.Add(new Vector2(j, dungeonSize.y - 1));
                    continue;
                }

                for (int i = 0; i < dungeonSize.x; i++)
                {
                    startPositions.Add(new Vector2(j, i));
                }
            }

            return startPositions[UnityEngine.Random.Range(0, startPositions.Count)];
        }

        public IEnumerator Generate(BaseDungeonGenerator generator)
        {
            DungeonGenerator dungeonGenerator = generator as DungeonGenerator;

            dungeonGenerator.startPosition = FindStartPosition(dungeonGenerator.DungeonSize);
            Direction direction = DirectionHandler.GetDirection();
            DirectionHandler.CheckDirection(ref direction, dungeonGenerator.startPosition, dungeonGenerator.DungeonSize);
            dungeonGenerator.GetRoomInfo(dungeonGenerator.startPosition);

            dungeonGenerator.StartRoom = Instantiate(_startRoomsList[0]).GetComponent<DungeonRoom>();
            dungeonGenerator.StartRoom.RoomInfo = dungeonGenerator.RoomList[0] as DungeonRoomInfo;
            dungeonGenerator.StartRoom.ApplyPosition(25f);

            yield return new PauseYield(dungeonGenerator);

            _isDone = true;
        }
    }
}