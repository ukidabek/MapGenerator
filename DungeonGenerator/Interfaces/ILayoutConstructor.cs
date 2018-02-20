using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapGenetaroion.Dungeon
{
    public interface ILayoutConstructor
    {
        List<IRoom> RoomList { get; set; }
        bool IsDone { get; }

        IEnumerator BuildLayout();
    }
}