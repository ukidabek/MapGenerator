using System;
using System.Collections;
using System.Collections.Generic;
using MapGenetaroion.BaseGenerator;
using UnityEngine;

namespace MapGenetaroion.DungeonGenerator.V2
{
    using Random = UnityEngine.Random;

    public class BuildLayoutPhase : BaseDungeonGenerationPhaseMonoBehaviour
    {
        private DungeonMetadata dungeonMetada = null;
        private GenerationSettings settings = null;

        public override IEnumerator Generate(LevelGenerator generator, object[] generationData)
        {
            dungeonMetada = LevelGenerator.GetMetaDataObject<DungeonMetadata>(generationData);
            settings = LevelGenerator.GetMetaDataObject<GenerationSettings>(generationData);

            for (int i = 0; i < dungeonMetada.RoomList.Count; i++)
            {
                BuildRoom(dungeonMetada.RoomList[i], i, generator.transform).SetUpWall(dungeonMetada.RoomList[i]);
                yield return new PauseYield(generator);
            }

            yield return new PauseYield(generator);

            _isDone = true;
        }

        private RoomSetup BuildRoom(DungeonMetadata.RoomInfo roomInfo, int index, Transform parent)
        {
            if(roomInfo.RoomObject != null)
                return roomInfo.RoomObject.GetComponent<RoomSetup>();

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

            roomInfo.RoomObject = Instantiate(roomPrefab, position, Quaternion.identity);
            roomInfo.RoomObject.name = string.Format("{0} {1}", index.ToString(), roomInfo.Type.ToString());

            roomInfo.RoomObject.transform.SetParent(parent);

            return roomInfo.RoomObject.GetComponent<RoomSetup>();
        }

        private GameObject RandomizePrefab(List<GameObject> roomPrefabList)
        {
            return roomPrefabList[Random.Range(0, roomPrefabList.Count)];
        }
    }
}