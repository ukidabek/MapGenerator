using System.Collections;
using System.Collections.Generic;
using MapGenetaroion.BaseGenerator;
using UnityEngine;

namespace MapGenetaroion.DungeonGenerator
{
    public class GenerateRoomListPhase : BaseDungeonGenerationPhaseMonoBehaviour
    {
        public override IEnumerator Generate(LevelGenerator generator, object[] generationData)
        {
            var dungeonMetada = LevelGenerator.GetMetaDataObject<DungeonMetadata>(generationData);
            dungeonMetada.RoomList.Clear();
            GenerateRoomList(dungeonMetada.StartRoom, dungeonMetada.RoomList);

            yield return new PauseYield(generator);

            _isDone = true;
        }

        private void GenerateRoomList(DungeonMetadata.RoomInfo startRoom, List<DungeonMetadata.RoomInfo> roomList)
        {
            roomList.Add(startRoom);

            for (int i = 0; i < startRoom.ConnectedRooms.Count; i++)
            {
                if (!roomList.Contains(startRoom.ConnectedRooms[i]))
                    GenerateRoomList(startRoom.ConnectedRooms[i], roomList);
            }
        }
    }
}