using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapGenetaroion.DungeonGenerator.V2
{
    public class GenerationSettings : MonoBehaviour
    {
        [SerializeField] private Vector2Int _size = new Vector2Int();
        public Vector2Int Size { get { return _size; } }

        [SerializeField] private Vector2 _roomSize = new Vector2(25f, 25f);
        public Vector2 RoomSize { get { return _roomSize; } }

        [SerializeField, Space] private int _minRoomsInLine = 3;
        public int MinRoomsInLine { get { return _minRoomsInLine; } }

        [SerializeField] private int _maxRoomsInLine = 6;
        public int MaxRoomsInLine { get { return _maxRoomsInLine; } }

        [SerializeField] private int _roomToGenerate = 25;
        public int RoomToGenerate { get { return _roomToGenerate; } }

        [SerializeField, Space] List<GameObject> _startRoomGameObject = new List<GameObject>();
        public List<GameObject> StartRoomGameObject { get { return _startRoomGameObject; } }

        [SerializeField] List<GameObject> _roomGameObject = new List<GameObject>();
        public List<GameObject> RoomGameObject { get { return _roomGameObject; } }

        [SerializeField] List<GameObject> _endRoomGameObject = new List<GameObject>();
        public List<GameObject> EndRoomGameObject { get { return _endRoomGameObject; } }
    }
}