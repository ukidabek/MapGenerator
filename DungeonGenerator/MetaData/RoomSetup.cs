using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapGenetaroion.DungeonGenerator
{
    public class RoomSetup : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _wallList = new List<GameObject>();
        [SerializeField] private List<GameObject> _doorsList = new List<GameObject>();

        public void SetUpWall(DungeonMetadata.RoomInfo info)
        {
            for (int i = 0; i < info.ConnectedRooms.Count; i++)
            {
                int index = (int)GetDirection(info,info.ConnectedRooms[i]);

                _wallList[index].SetActive(false);
                _doorsList[index].SetActive(true);
            }
        }

        private GenerateLayoutPhase.Direction GetDirection(DungeonMetadata.RoomInfo info, DungeonMetadata.RoomInfo neighborInfo)
        {
            if (info.Position.x > neighborInfo.Position.x)
                return GenerateLayoutPhase.Direction.Down;
            if (info.Position.x < neighborInfo.Position.x)
                return GenerateLayoutPhase.Direction.Up;
            if (info.Position.y < neighborInfo.Position.y)
                return GenerateLayoutPhase.Direction.Right;
            if (info.Position.y > neighborInfo.Position.y)
                return GenerateLayoutPhase.Direction.Left;

            return GenerateLayoutPhase.Direction.Down;
        }
    }
}