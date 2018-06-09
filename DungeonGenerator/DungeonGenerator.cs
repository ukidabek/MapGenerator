using UnityEngine;

using System.Collections;
using System.Collections.Generic;

using MapGenetaroion.BaseGenerator;

namespace MapGenetaroion.DungeonGenerator
{
    public class DungeonGenerator : BaseDungeonGenerator
    {
        [SerializeField, Space] protected Vector2Int _dungeonSize = new Vector2Int();

        [Space] public DungeonRoom StartRoom = null;
        public Vector2 startPosition = Vector3.zero;

        public List<IRoomInfo> RoomList = new List<IRoomInfo>();

        public IRoomInfo GetRoomInfo(Vector2 position)
        {
            RoomList.Add(new DungeonRoomInfo(_dungeonSize.x - position.x, position.y) as IRoomInfo);
            return RoomList[RoomList.Count - 1];
        }

        protected override void InitializePhase()
        {
            _generationPhaseList[_phaseIndex].DungeonSize = _dungeonSize;
            base.InitializePhase();
        }

        //protected override void GoToNextPhase()
        //{
        //    _generationPhaseList[_phaseIndex + 1].RoomList = _generationPhaseList[_phaseIndex].RoomList;

        //    base.GoToNextPhase();
        //}
    }
}