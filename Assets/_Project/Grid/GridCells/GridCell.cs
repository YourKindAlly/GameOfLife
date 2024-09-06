using UnityEngine;

namespace GameOfLife.Grid
{
    public class GridCell
    {
        public Vector2Int PositionInGrid;

        public GridCell(Vector2Int positionInGrid)
        {
            PositionInGrid = positionInGrid;
        }
    }
}
