using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapGenetaroion.DungeonGenerator.V2
{
    public class GenerationSettings : MonoBehaviour
    {
        [SerializeField] private Vector2Int _size = new Vector2Int();
        public Vector2Int Size { get { return _size; } }
    }
}