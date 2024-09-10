using UnityEngine;

namespace GameOfLife.Grid
{
    public class Empty : GridCell
    {
        public bool IsInhabitable;

        public Empty(Vector2Int positionInGrid) : base(positionInGrid) { }
    }
}
