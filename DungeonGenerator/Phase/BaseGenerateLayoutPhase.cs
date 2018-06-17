using MapGenetaroion.BaseGenerator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapGenetaroion.DungeonGenerator
{
    public abstract class BaseGenerateLayoutPhase : BaseDungeonGenerationPhaseMonoBehaviour
    {
        protected DungeonMetadata dungeonMetada;
        protected GenerationSettings settings;

        public enum Direction
        {
            Up,
            Right,
            Down,
            Left
        }

        protected Direction GetDirection()
        {
            return (Direction)Random.Range(0, 4);
        }

        protected void GetReference(object[] generationData)
        {
            dungeonMetada = LevelGenerator.GetMetaDataObject<DungeonMetadata>(generationData);
            settings = LevelGenerator.GetMetaDataObject<GenerationSettings>(generationData);
        }

        protected int GetRoomCount()
        {
            return Random.Range(settings.MinRoomsInLine, settings.MaxRoomsInLine);
        }

        protected bool CheckDirection(Direction direction, Vector2 currentPosition, Layout layoutData)
        {
            switch (direction)
            {
                case Direction.Up:
                    return CanGo(1, 0, currentPosition, layoutData);
                case Direction.Right:
                    return CanGo(0, 1, currentPosition, layoutData);
                case Direction.Down:
                    return CanGo(-1, 0, currentPosition, layoutData);
                case Direction.Left:
                    return CanGo(0, -1, currentPosition, layoutData);
            }

            return false;
        }

        protected bool CheckBlock(Vector2 currentPosition, Layout layoutData)
        {
            return
                !CanGo(1, 0, currentPosition, layoutData) &&
                !CanGo(0, 1, currentPosition, layoutData) &&
                !CanGo(-1, 0, currentPosition, layoutData) &&
                !CanGo(0, -1, currentPosition, layoutData);
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

        protected Vector2 Move(Direction direction, Vector2 currentPosition)
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

        protected void AddRoom(ref Vector2 currentPosition, Direction direction, ref DungeonMetadata.RoomInfo currentRoom, ref Layout layout, DungeonMetadata.RoomInfo.RoomType roomType = DungeonMetadata.RoomInfo.RoomType.Normal)
        {
            currentPosition = Move(direction, currentPosition);
            layout[currentPosition] = true;
            var newRoom = new DungeonMetadata.RoomInfo(currentPosition, roomType);
            currentRoom.ConnectedRooms.Add(newRoom);
            newRoom.ConnectedRooms.Add(currentRoom);
            currentRoom = newRoom;
        }
    }
}