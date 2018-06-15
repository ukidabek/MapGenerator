using System;
using System.Collections;
using System.Collections.Generic;
using MapGenetaroion.BaseGenerator;
using UnityEngine;

namespace MapGenetaroion.DungeonGenerator.V2
{
    using Random = UnityEngine.Random;

    public class GenerateLayoutPhase : BaseDungeonGenerationPhase
    {
        private DungeonMetadata dungeonMetada;
        private GenerationSettings settings;

        private enum Direction
        {
            Up,
            Right,
            Down,
            Left
        }

        private Direction GetDirection()
        {
            return (Direction)Random.Range(0, 4);
        }

        private int GetRoomCount()
        {
            return Random.Range(settings.MinRoomsInLine, settings.MaxRoomsInLine);
        }

        public override IEnumerator Generate(LevelGenerator generator, object[] generationData)
        {
            dungeonMetada = LevelGenerator.GetMetaDataObject<DungeonMetadata>(generationData);
            settings = LevelGenerator.GetMetaDataObject<GenerationSettings>(generationData);

            var layout = dungeonMetada.LayoutData;
            var currentRoom = dungeonMetada.StartRoom;

            Vector2 currentPosition = dungeonMetada.StartRoom.Position;
            Direction direction = GetDirection();
            int roomsInline = GetRoomCount();
            int roomToGenerate = settings.RoomToGenerate;
            bool isBloced = false;

            while(roomToGenerate > 0)
            {
                for (int i = 0; i < roomsInline; i++)
                {
                    if (CheckDirection(direction, currentPosition, layout))
                    {
                        currentPosition = Move(direction, currentPosition);
                        layout[currentPosition] = true;
                        var newRoom = new DungeonMetadata.RoomInfo(currentPosition);
                        currentRoom.ConnectedRooms.Add(newRoom);
                        currentRoom = newRoom;
                        roomToGenerate--;
                    }
                    else
                    {
                        isBloced = CheckBlock(currentPosition, layout);
                        if (isBloced)
                            break;
                        i++;
                        direction = GetDirection();
                    }

                    yield return new PauseYield(generator);
                }

                if (isBloced)
                    break;
                direction = GetDirection();
                yield return new PauseYield(generator);
            }

            currentRoom.Type = DungeonMetadata.RoomInfo.RoomType.End;
            _isDone = true;
        }

        private bool CheckDirection(Direction direction, Vector2 currentPosition, Layout layoutData)
        {
            switch (direction)
            {
                case Direction.Up:
                    return CanGo( 1, 0, currentPosition, layoutData);
                case Direction.Right:
                    return CanGo( 0, 1, currentPosition, layoutData);
                case Direction.Down:
                    return CanGo(-1, 0, currentPosition, layoutData);
                case Direction.Left:
                    return CanGo(0, -1, currentPosition, layoutData);
            }

            return false;
        }

        private bool CheckBlock(Vector2 currentPosition, Layout layoutData)
        {
            return
                !CanGo( 1, 0, currentPosition, layoutData) &&
                !CanGo( 0, 1, currentPosition, layoutData) &&
                !CanGo(-1, 0, currentPosition, layoutData) &&
                !CanGo( 0, -1, currentPosition, layoutData);

        }

        private bool CanGo(int x, int y, Vector2 currentPosition, Layout layoutData)
        {
            Vector2 positionToCheck = new Vector2(currentPosition.x + x, currentPosition.y + y);

            if (positionToCheck.x < 0 || positionToCheck.x > layoutData.RowsCount - 1)
                return false;
            else if (positionToCheck.y < 0 || positionToCheck.y > layoutData.ColumnsCount - 1)
                return false;
            else return !layoutData[positionToCheck];
        }

        private Vector2 Move(Direction direction, Vector2 currentPosition)
        {
            switch (direction)
            {
                case Direction.Up:
                    currentPosition.x += 1;
                    break;
                case Direction.Right:
                    currentPosition.y += 1;
                    break;
                case Direction.Down:
                    currentPosition.x -= 1;
                    break;
                case Direction.Left:
                    currentPosition.y -= 1;
                    break;
            }

            return currentPosition;
        }
    }
}