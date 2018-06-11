using UnityEngine;

using System.Collections;
using System.Collections.Generic;

using MapGenetaroion.BaseGenerator;
using System;

namespace MapGenetaroion.DungeonGenerator.Beta
{
    using Random = UnityEngine.Random;

    public class DungeonLayoutGenerator : MonoBehaviour, IGenerationPhase
    {
        [SerializeField] private bool _isDone = false;
        public bool IsDone { get { return _isDone; } }

        private Vector2Int DungeonSize { get; set; }

        private bool[,] _layout = null;
        public bool[,] Layout { get { return _layout; } }
        private Direction[,] _layoutDirection = null;
        public Direction[,] LayoutDirection { get { return _layoutDirection; } }

        [SerializeField] private int _roomCount = 10;

        [SerializeField] private int _minRoomsInLine = 1;

        [SerializeField] private int _maxRoomInLine = 3;

        [SerializeField] private int _roomsToGenerate = 0;

        [SerializeField] private bool _generateCorridors = false;

        [SerializeField] private int _minCorridorCount = 3;

        [SerializeField] private int _maxCorridorCount = 6;

        private Vector2 currentPosition = Vector2.zero;
        private Direction currentDirection;

        private Vector2 _startPosition = Vector2.zero;
        public Vector2 StartPosition { get { return _startPosition; } }

        private Vector2 _lastPosition = Vector2.zero;
        public Vector2 LastPosition { get { return _lastPosition; } }

        private int _generatedRooms = 0;

        public bool Pause { get { return false; } }


        private int GetRoomInLine()
        {
            return Random.Range(_minRoomsInLine, _maxRoomInLine + 1);
        }

        public bool GenerationBlocked(Vector2 point)
        {
            int availableDirections = 4;
            int directionBlocked = -1;
            for (int y = -1; y <= 1; y++)
            {
                for (int x = -1; x <= 1; x++)
                {
                    if (x == 0 || y == 0)
                    {
                        if ((int)point.x + y < 0 ||
                            (int)point.x + y == DungeonSize.y ||
                            (int)point.y + x < 0 ||
                            (int)point.y + x == DungeonSize.x)
                        {
                            availableDirections--;
                        }
                        else
                        {
                            if (_layout[(int)point.x + y, (int)point.y + x])
                                directionBlocked++;
                        }
                    }
                }
            }
            return directionBlocked == availableDirections;
        }

        private bool Move(Direction direction, ref Vector2 currentPosition)
        {
            switch (direction)
            {
                case Direction.Up:
                    if (currentPosition.x - 1 >= 0 &&
                        !_layout[(int)currentPosition.x - 1, (int)currentPosition.y])
                    {
                        currentPosition.x -= 1;
                        return true;
                    }
                    return false;

                case Direction.Right:
                    if (currentPosition.y + 1 < DungeonSize.y &&
                        !_layout[(int)currentPosition.x, (int)currentPosition.y + 1])
                    {
                        currentPosition.y += 1;
                        return true;
                    }
                    return false;

                case Direction.Down:
                    if (currentPosition.x + 1 < DungeonSize.x &&
                        !_layout[(int)currentPosition.x + 1, (int)currentPosition.y])
                    {
                        currentPosition.x += 1;
                        return true;
                    }
                    return false;

                case Direction.Left:
                    if (currentPosition.y - 1 >= 0 &&
                        !_layout[(int)currentPosition.x, (int)currentPosition.y - 1])
                    {
                        currentPosition.y -= 1;
                        return true;
                    }
                    return false;
            }
            return false;
        }

        public IEnumerator Generate(LevelGenerator generator, params object[] generationData)
        {
            DungeonSize = (generator as DungeonGenerator).DungeonSize;
            _isDone = false;
            _layout = new bool[DungeonSize.x, DungeonSize.y];
            _layoutDirection = new Direction[DungeonSize.x, DungeonSize.y];
            _roomsToGenerate = _roomCount - 2;

            currentDirection = DirectionHandler.GetDirection();

            currentPosition = _startPosition = (generator as DungeonGenerator).startPosition;
            _layout[(int)currentPosition.x, (int)currentPosition.y] = true;

            int roomsInLine = GetRoomInLine();

            while (_roomsToGenerate > 0)
            {
                while (roomsInLine > 0)
                {
                    if (_roomsToGenerate == 0)
                        break;

                    if (Move(currentDirection, ref currentPosition))
                    {
                        _lastPosition = currentPosition;
                        UpdateLayout(currentPosition, _layoutDirection);

                        (generator as DungeonGenerator).GetRoomInfo(currentPosition);

                        --roomsInLine;
                        --_roomsToGenerate;

                        yield return new PauseYield(generator);
                    }
                    else
                    {
                        break;
                    }
                }

                if (_roomsToGenerate > 0 && GenerationBlocked(_lastPosition))
                {
                    break;
                }

                currentDirection = DirectionHandler.GetDirection();
                roomsInLine = GetRoomInLine();

                yield return new PauseYield(generator);
            }

            _generatedRooms = (generator as DungeonGenerator).RoomList.Count - 2;

            if(_generateCorridors)
            {
                int corridorsToGanerate = Random.Range(_minCorridorCount, _maxCorridorCount);

                while (corridorsToGanerate > 0)
                {
                    var roomList = (generator as DungeonGenerator).RoomList;
                    currentPosition = GetCorridorStart(generator);
                    currentDirection = DirectionHandler.GetDirection();

                    roomsInLine = GetRoomInLine();

                    List<IRoomInfo> corridor = new List<IRoomInfo>();
                    corridor.Add((generator as DungeonGenerator).CreateNewRoomForCorridor(currentPosition));
                    //UpdateLayout(currentPosition, _layoutDirection);

                    while (roomsInLine > 0)
                    {
                        if (Move(currentDirection, ref currentPosition))
                        {
                            UpdateLayout(currentPosition, _layoutDirection);

                            corridor.Add((generator as DungeonGenerator).CreateNewRoomForCorridor(currentPosition));
                            roomsInLine--;
                            yield return new PauseYield(generator);
                        }
                        else
                        {
                            if (GenerationBlocked(currentPosition))
                            {
                                break;
                            }
                            else
                            {
                                 currentDirection = DirectionHandler.GetDirection();
                            }

                            yield return new PauseYield(generator);
                        }

                    }

                    (generator as DungeonGenerator).CorridorsList.Add(corridor);
                    corridorsToGanerate--;
                }
            }

            _isDone = true;
        }

        private void UpdateLayout(Vector2 currentPosition, Direction[,] layoutDirection)
        {
            _layout[(int)currentPosition.x, (int)currentPosition.y] = true;
            _layoutDirection[(int)currentPosition.x, (int)currentPosition.y] = currentDirection;
        }

        private Vector2 GetCorridorStart(LevelGenerator generator)
        {
            var roomList = (generator as DungeonGenerator).RoomList;
            int index = Random.Range(1, _generatedRooms);
            return (roomList[index] as DungeonRoomInfo).Position;
        }
    }
}