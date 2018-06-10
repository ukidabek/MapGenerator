using UnityEngine;

using System.Collections;
using System.Collections.Generic;

namespace MapGenetaroion.DungeonGenerator.Beta
{
    public static class DirectionHandler
    {
        public static Direction GetDirection()
        {
            Direction direction = (Direction)Random.Range(0, 4);
            return direction;
        }

        public static Direction GetOppositeDirection(Direction direction)
        {
            if (direction == Direction.Up || direction == Direction.Right)
                return direction + 2;

            if (direction == Direction.Left || direction == Direction.Down)
                return direction - 2;

            return direction;
        }

        public static void CheckDirection(ref Direction direction, Vector2 currentPosition, Vector2Int size)
        {
            if ((direction == Direction.Up && currentPosition.x == 0) ||
                (direction == Direction.Down && currentPosition.x == size.y - 1) ||
                (direction == Direction.Left && currentPosition.y == 0) ||
                (direction == Direction.Down && currentPosition.y == size.x - 1))
            {
                direction = GetOppositeDirection(direction);
            }
        }

    }
}
