﻿using System;
using System.Collections;
using System.Collections.Generic;
using MapGenetaroion.BaseGenerator;
using UnityEngine;

namespace MapGenetaroion.DungeonGenerator.V2
{
    using Random = UnityEngine.Random;

    public class BuildLayoutPhase : BaseDungeonGenerationPhase
    {
        private DungeonMetadata dungeonMetada = null;
        private GenerationSettings settings = null;

        public override IEnumerator Generate(LevelGenerator generator, object[] generationData)
        {
            dungeonMetada = LevelGenerator.GetMetaDataObject<DungeonMetadata>(generationData);
            settings = LevelGenerator.GetMetaDataObject<GenerationSettings>(generationData);

            List<DungeonMetadata.RoomInfo> roomList = new List<DungeonMetadata.RoomInfo>();
            GenerateRoomList(dungeonMetada.StartRoom, roomList);

            for (int i = 0; i < roomList.Count; i++)
            {
                BuildRoom(roomList[i],i).SetUpWall(roomList[i]);
                yield return new PauseYield(generator);
            }

            yield return new PauseYield(generator);


            _isDone = true;
        }

        private RoomSetup BuildRoom(DungeonMetadata.RoomInfo roomInfo, int index)
        {
            GameObject roomPrefab = null;
            switch (roomInfo.Type)
            {
                case DungeonMetadata.RoomInfo.RoomType.Start:
                    roomPrefab = RandomizePrefab(settings.StartRoomGameObject);
                    break;
                case DungeonMetadata.RoomInfo.RoomType.Normal:
                case DungeonMetadata.RoomInfo.RoomType.Corridor:
                    roomPrefab = RandomizePrefab(settings.RoomGameObject);
                    break;
                case DungeonMetadata.RoomInfo.RoomType.End:
                    roomPrefab = RandomizePrefab(settings.EndRoomGameObject);
                    break;
            }

            var position = new Vector3(roomInfo.Position.y * settings.RoomSize.y, 0, roomInfo.Position.x * settings.RoomSize.x);

            var instance = Instantiate(roomPrefab, position, Quaternion.identity);
            instance.name = index.ToString();

            return instance.GetComponent<RoomSetup>();
        }

        private GameObject RandomizePrefab(List<GameObject> roomPrefabList)
        {
            return roomPrefabList[Random.Range(0, roomPrefabList.Count)];
        }

        private void GenerateRoomList(DungeonMetadata.RoomInfo startRoom, List<DungeonMetadata.RoomInfo> roomList)
        {
            //List<DungeonMetadata.RoomInfo> roomList = new List<DungeonMetadata.RoomInfo>();
            roomList.Add(startRoom);

            for (int i = 0; i < startRoom.ConnectedRooms.Count; i++)
            {
                if(!roomList.Contains(startRoom.ConnectedRooms[i]))
                    GenerateRoomList(startRoom.ConnectedRooms[i], roomList);
            }

            //return roomList;
        }
    }
}