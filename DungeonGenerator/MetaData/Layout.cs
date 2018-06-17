using System;
using UnityEngine;

namespace MapGenetaroion.DungeonGenerator
{
    [Serializable]
    public class Layout
    {
        [Serializable]
        private struct Row
        {
            public bool[] Columns;

            public Row(int size)
            {
                Columns = new bool[size];
            }
        }

        [SerializeField] private Row[] Rows = null;

        public int ColumnsCount { get; private set; }
        public int RowsCount { get; private set; }

        public Layout(Vector2Int size) : this(size.x, size.y) { }

        public Layout(int rows, int columns)
        {
            RowsCount = rows;
            ColumnsCount = columns;

            Rows = new Row[rows];
            for (int i = 0; i < Rows.Length; i++)
            {
                Rows[i] = new Row(columns);
            }
        }

        public bool this[int row, int column]
        {
            get { return Rows[row].Columns[column]; }
            set { Rows[row].Columns[column] = value; }
        }

        public bool this[Vector2Int position]
        {
            get { return this[position.x, position.y]; }
            set { this[position.x, position.y] = value; }
        }

        public bool this[Vector2 position]
        {
            get { return this[(int)position.x, (int)position.y]; }
            set { this[(int)position.x, (int)position.y] = value; }
        }

    }
}