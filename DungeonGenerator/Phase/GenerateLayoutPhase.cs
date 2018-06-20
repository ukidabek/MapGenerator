using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

using MapGenetaroion.BaseGenerator;

namespace MapGenetaroion.DungeonGenerator
{
    public class GenerateLayoutPhase : BaseGenerateLayoutPhase
    {
        public override IEnumerator Generate(LevelGenerator generator, object[] generationData)
        {
            GetReference(generationData);

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
                        AddRoom(ref currentPosition, direction, ref currentRoom, ref layout);
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

            dungeonMetada.EndRoom = currentRoom;
            currentRoom.Type = DungeonMetadata.RoomInfo.RoomType.End;

            _isDone = true;
        }
    }
}