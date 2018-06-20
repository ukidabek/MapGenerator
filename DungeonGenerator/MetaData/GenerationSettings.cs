using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MapGenetaroion.DungeonGenerator
{
    public class GenerationSettings : MonoBehaviour, ObjectsPlacerPhase.IObjectPlacerPhaseConnfigProvider
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

        [SerializeField] private int _minCorridorsToGenerate = 2;
        public int MinCorridorsToGenerate { get { return _minCorridorsToGenerate; } }

        [SerializeField] private int _maxCorridorsToGenerate = 4;
        public int MaxCorridorsToGenerate { get { return _maxCorridorsToGenerate; } }

        [SerializeField] private int _minCorridorsToLenght = 2;
        public int MinCorridorsToLenght { get { return _minCorridorsToLenght; } }

        [SerializeField] private int _maxCorridorsToLenght = 4;
        public int MaxCorridorsToLenght { get { return _maxCorridorsToLenght; } }

        [SerializeField, Space] List<GameObject> _startRoomGameObject = new List<GameObject>();
        public List<GameObject> StartRoomGameObject { get { return _startRoomGameObject; } }

        [SerializeField] List<GameObject> _roomGameObject = new List<GameObject>();
        public List<GameObject> RoomGameObject { get { return _roomGameObject; } }

        [SerializeField] List<GameObject> _endRoomGameObject = new List<GameObject>();
        public List<GameObject> EndRoomGameObject { get { return _endRoomGameObject; } }


        [SerializeField] List<ObjectsPlacerPhase.ObjectPlacerPhaseConnfig> _objectPlacerPhaseConnfigs = new List<ObjectsPlacerPhase.ObjectPlacerPhaseConnfig>();
        public ObjectsPlacerPhase.ObjectPlacerPhaseConnfig this[int key]
        {
            get
            {
                for (int i = 0; i < _objectPlacerPhaseConnfigs.Count; i++)
                {
                    if (_objectPlacerPhaseConnfigs[i].PhaseIndex == key)
                        return _objectPlacerPhaseConnfigs[i];
                }

                return null;
            }
        }
    }
}