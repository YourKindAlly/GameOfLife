using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace GameOfLife.Grid
{
    public class GameGrid : MonoBehaviour
    {
        [SerializeField] private int mapSize = 20;
        [SerializeField] private float cellSize;
        private GridCell[,] grid;
        
        private List<Inhabited> inhabitedCells = new();
        private List<GameObject> inhabitedGameObjects = new();

        [SerializeField] private GameObject inhabitedPrefab;

        private void Start()
        {
            grid = new GridCell[mapSize, mapSize];
            
            GenerateGrid();

            StartCoroutine(GenerationTick());
        }

        private GameObject SetUpCell(GameObject prefab, int x, int y)
        {
            GameObject cellObject = Instantiate(prefab, transform);
            
            cellObject.transform.position = new Vector3(x, y) * cellSize;
            cellObject.transform.localScale = new Vector3(cellSize, cellSize);
            
            return cellObject;
        }

        private void GenerateGrid()
        {
            for (int y = 0; y < mapSize; y++)
            {
                for (int x = 0; x < mapSize; x++)
                {
                    grid[x, y] = UnityEngine.Random.Range(0, 5) == 0
                        ? new Inhabited(new Vector2Int(x, y))
                        : new Empty(new Vector2Int(x, y));
                }
            }
        }
        
        private IEnumerator GenerationTick()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                
                CheckGridForNextTick();
                UpdateGridCells();
            }
        }

        private void CheckGridForNextTick()
        {
            foreach (GridCell gridCell in grid)
            {
                int neighbours = 0;
                
                foreach (Inhabited otherCell in inhabitedCells)
                {
                    if (gridCell == otherCell)
                    {
                        continue;
                    }

                    if (gridCell.PositionInGrid.x - otherCell.PositionInGrid.x is 1 or -1 ||
                        gridCell.PositionInGrid.y - otherCell.PositionInGrid.y is 1 or -1)
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

        private void UpdateGridCells()
        {
            foreach (GridCell gridCell in grid)
            {
                if (gridCell is Empty { IsInhabitable: true })
                {
                    Inhabited newCell = new(gridCell.PositionInGrid);
                    grid[gridCell.PositionInGrid.x, gridCell.PositionInGrid.y] = newCell;
                    inhabitedCells.Add(newCell);
                    
                    GameObject newGameObject = Instantiate(inhabitedPrefab, transform);
                    newGameObject.transform.position = new Vector2(gridCell.PositionInGrid.x, gridCell.PositionInGrid.y) * cellSize;
                    
                    inhabitedGameObjects.Add(newGameObject);
                }
                else if (gridCell is Inhabited { IsDying: true })
                {
                    grid[gridCell.PositionInGrid.x, gridCell.PositionInGrid.y] = new Empty(gridCell.PositionInGrid);

                    for (int index = 0; index < inhabitedCells.Count; index++)
                    {
                        if (inhabitedGameObjects[index].transform.position == new Vector3(gridCell.PositionInGrid.x, gridCell.PositionInGrid.y) * cellSize)
                        {
                            Destroy(inhabitedGameObjects[index]);
                            inhabitedGameObjects.RemoveAt(index);
                        }
                    }
                }
            }
        }
    }
}
