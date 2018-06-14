using UnityEngine;

using System.Collections;
using System.Collections.Generic;

using MapGenetaroion.BaseGenerator;

namespace MapGenetaroion.DungeonGenerator.V2
{
    public class SelectStartRoomPhase : BaseDungeonGenerationPhase
    {
        public override IEnumerator Generate(LevelGenerator generator, object[] generationData)
        {
            var dungeonMetada = LevelGenerator.GetMetaDataObject<DungeonMetadata>(generationData);
            var layout = dungeonMetada.LayoutData;

            List<Vector2Int> startPositionsList = new List<Vector2Int>();
            for (int i = 0; i < layout.RowsCount; i++)
            {
                if(layout.RowsCount == 0 || i == layout.RowsCount -1)
                    for (int j = 0; j < layout.ColumnsCount; j++)
                        startPositionsList.Add(new Vector2Int(i, j));
                else
                {
                    startPositionsList.Add(new Vector2Int(i, 0));
                    startPositionsList.Add(new Vector2Int(i, layout.ColumnsCount - 1));
                }
            }
            
            var startRoomPosition = startPositionsList[Random.Range(0, startPositionsList.Count - 1)];

            dungeonMetada.StartRoom = new DungeonMetadata.RoomInfo(startRoomPosition);

            layout[startRoomPosition] = true;

            yield return new PauseYield(generator);

            _isDone = true;
        }
    }
}