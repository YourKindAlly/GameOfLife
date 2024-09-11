using UnityEngine;
using System.Collections.Generic;

namespace GameOfLife.Grid
{
    public class GameGrid : MonoBehaviour
    {
        [field: SerializeField] public int MapSize { get; private set; } = 50;
        [field: SerializeField] public float CellSize { get; private set; } = 0.1f;
        
        private GridCell[,] grid;
        
        private Dictionary<Inhabited, GameObject> inhabitedCells = new();
        [SerializeField] private GameObject inhabitedPrefab;

        private void Start()
        {
            grid = new GridCell[MapSize, MapSize];
            
            GenerateGrid();
        }
        

        private GameObject SetUpCell(int x, int y)
        {
            GameObject cellObject = Instantiate(inhabitedPrefab, transform);

            cellObject.name = $"Inhabitable {x}, {y}";
            cellObject.transform.position = new Vector3(x, y) * CellSize;
            cellObject.transform.localScale = new Vector3(CellSize, CellSize) * 0.9f;
            
            return cellObject;
        }

        public void GenerateGrid()
        {
            for (int y = 0; y < MapSize; y++)
            {
                for (int x = 0; x < MapSize; x++)
                {
                    if (Random.Range(0, 5) == 0)
                    {
                        Inhabited inhabitedCell = new(new Vector2Int(x, y));
                        
                        GameObject inhabitedGameObject = SetUpCell(x, y);
                        
                        inhabitedCells.Add(inhabitedCell, inhabitedGameObject);
                        grid[x, y] = inhabitedCell;
                    }
                    else
                    {
                        Empty emptyCell = new(new Vector2Int(x, y));
                        grid[x, y] = emptyCell;
                    }
                }
            }
        }
        
        public void CheckGridForNextTick()
        {
            foreach (GridCell gridCell in grid)
            {
                int neighbours = 0;
                
                foreach (KeyValuePair<Inhabited, GameObject> otherCell in inhabitedCells)
                {
                    if (gridCell.PositionInGrid == otherCell.Key.PositionInGrid)
                    {
                        continue;
                    }
                    
                    if (gridCell.PositionInGrid.x - otherCell.Key.PositionInGrid.x is 0 or 1 or -1 && gridCell.PositionInGrid.y - otherCell.Key.PositionInGrid.y is 0 or 1 or -1)
                    {
                        neighbours++;
                    }
                }

                if (gridCell is Empty emptyCell && neighbours == 3)
                {
                    emptyCell.IsInhabitable = true;
                }
                else if (gridCell is Inhabited inhabitedCell)
                {
                    if (neighbours is < 2 or > 3)
                    {
                        inhabitedCell.IsDying = true;
                    }
                }
            }
        }

        public void UpdateGridCells()
        {
            foreach (GridCell gridCell in grid)
            {
                if (gridCell is Empty { IsInhabitable: true })
                {
                    Inhabited newCell = new(gridCell.PositionInGrid);
                    grid[gridCell.PositionInGrid.x, gridCell.PositionInGrid.y] = newCell;
                    
                    GameObject newGameObject = SetUpCell(gridCell.PositionInGrid.x, gridCell.PositionInGrid.y);
                    newGameObject.transform.position = new Vector2(gridCell.PositionInGrid.x, gridCell.PositionInGrid.y) * CellSize;
                    inhabitedCells.Add(newCell, newGameObject);
                }
                else if (gridCell is Inhabited { IsDying: true } dyingCell)
                {
                    grid[dyingCell.PositionInGrid.x, dyingCell.PositionInGrid.y] = new Empty(dyingCell.PositionInGrid);
                    
                    Destroy(inhabitedCells[dyingCell]);
                    inhabitedCells.Remove(dyingCell);
                }
            }
        }
    }
}
