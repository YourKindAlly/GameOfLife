using UnityEngine;
using System.Collections.Generic;

namespace GameOfLife.Grid
{
    public class GameGrid : MonoBehaviour
    {
        [field: SerializeField] public int MapSize { get; private set; } = 50;
        [field: SerializeField] public float CellSize { get; private set; } = 0.1f;
        
        private GridCell[,] grid;
        private List<GridCell> inhabitedCells = new();

        [SerializeField] private GameObject cellPrefab;


        private void Start()
        {
            grid = new GridCell[MapSize, MapSize];
            
            GenerateGrid();
        }
        

        private GridCell SetUpCell(CellType type, int x, int y)
        {
            GameObject cellGameObject = Instantiate(cellPrefab, transform);

            GridCell cell = cellGameObject.AddComponent<GridCell>();
            cell.CurrentType = type;

            if (cell.CurrentType == CellType.Empty)
                cell.Sprite.enabled = false;
            
            cellGameObject.name = $"Cell {x}, {y}";
            cellGameObject.transform.position = new Vector3(x, y) * CellSize;
            cellGameObject.transform.localScale = new Vector3(CellSize, CellSize) * 0.9f;
            
            return cell;
        }

        private void GenerateGrid()
        {
            for (int y = 0; y < MapSize; y++)
            {
                for (int x = 0; x < MapSize; x++)
                {
                    GridCell cell = SetUpCell(Random.Range(0, 5) == 0 ? CellType.Inhabited : CellType.Empty, x, y);
                    
                    if (cell.CurrentType == CellType.Inhabited)
                        inhabitedCells.Add(cell);

                    grid[x, y] = cell;
                }
            }
        }
        
        public void CheckGridForNextTick()
        {
            foreach (GridCell cell in grid)
            {
                int neighbours = 0;
                
                foreach (GridCell otherCell in inhabitedCells)
                {
                    if (cell.positionInGrid == otherCell.positionInGrid)
                    {
                        continue;
                    }
                    
                    if (cell.positionInGrid.x - otherCell.positionInGrid.x is 0 or 1 or -1 && cell.positionInGrid.y - otherCell.positionInGrid.y is 0 or 1 or -1)
                    {
                        neighbours++;
                    }
                }

                if (cell.CurrentType == CellType.Empty && neighbours == 3)
                {
                   cell.EnableBornState();
                }
                else if (cell.CurrentType == CellType.Inhabited && neighbours is < 2 or > 3)
                {
                    cell.EnableDyingState();
                }
            }
        }

        public void UpdateGridCells()
        {
            foreach (GridCell cell in grid)
            {
                if (!cell.IsDying && !cell.IsBorn)
                    return;
                
                if (cell.CurrentType == CellType.Inhabited)
                {
                    inhabitedCells.Add(cell);
                }
                else if (cell.CurrentType == CellType.Empty)
                {
                    inhabitedCells.Remove(cell);
                }
                
                cell.ChangeType();
                cell.ChangeSprite();
            }
        }
    }
}
