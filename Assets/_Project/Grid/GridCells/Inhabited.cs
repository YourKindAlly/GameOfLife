using UnityEngine;

namespace GameOfLife.Grid
{
    public class Inhabited : GridCell
    {
        public bool IsDying;

        public Inhabited(Vector2Int positionInGrid) : base(positionInGrid) { }
    }
}
