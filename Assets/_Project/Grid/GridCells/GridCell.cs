using UnityEngine;

namespace GameOfLife.Grid
{
    public abstract class GridCell
    {
        public Vector2Int PositionInGrid;

        protected GridCell(Vector2Int positionInGrid)
        {
            PositionInGrid = positionInGrid;
        }
    }
}
