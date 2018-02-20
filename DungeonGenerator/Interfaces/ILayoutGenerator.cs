using UnityEngine;

using System.Collections;
using System.Collections.Generic;

namespace MapGenetaroion.Dungeon
{
    public interface ILayoutGenerator
    {
        List<IRoom> RoomList { get; }
        bool IsDone { get; }
        IEnumerator GenerateLayout();
    }
}