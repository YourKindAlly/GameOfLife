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
        private List<Populated> populatedCells = new();

        [SerializeField] private GameObject populatedPrefab;
        [SerializeField] private GameObject emptyCellPrefab;

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
                    GameObject cellObject;
                    
                    if (UnityEngine.Random.Range(0, 5) == 0)
                    {
                        cellObject = SetUpCell(populatedPrefab, x, y);
                        grid[x, y] = cellObject.GetComponent<Populated>();
                        populatedCells.Add(cellObject.GetComponent<Populated>());
                    }
                    else
                    {
                        cellObject = SetUpCell(emptyCellPrefab, x, y);
                        grid[x, y] = cellObject.GetComponent<Empty>();
                    }
                    
                    cellObject.GetComponent<GridCell>().positionInGrid = new Vector2Int(x, y);
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
            foreach (GridCell cell in grid)
            {
                List<Populated> neighbours = new();

                foreach (Populated otherCell in populatedCells)
                {
                    if (cell.positionInGrid == otherCell.positionInGrid)
                        continue;

                    if (Math.Abs(cell.positionInGrid.x - otherCell.positionInGrid.x) == 1 ||
                        Math.Abs(cell.positionInGrid.y - otherCell.positionInGrid.y) == 1)
                        neighbours.Add(otherCell);
                }
                
                if ((neighbours.Count < 2 || neighbours.Count > 3) && cell.TryGetComponent(out Populated populatedCell))
                {
                    populatedCell.isDying = true;
                }
                else if (neighbours.Count == 3 && cell.TryGetComponent(out Empty emptyCell))
                {
                    emptyCell.isInhabitable = true;
                }
            }
        }

        private void UpdateGridCells()
        {
            List<Populated> temporaryList = new();
            
            foreach (GridCell cell in grid)
            {
                if (cell.TryGetComponent(out Empty emptyCell))
                {
                    if (!emptyCell.isInhabitable)
                        continue;
                    
                    GameObject newPopulatedCell = SetUpCell(populatedPrefab, cell.positionInGrid.x, cell.positionInGrid.y);
                    grid[cell.positionInGrid.x, cell.positionInGrid.y] = newPopulatedCell.GetComponent<Populated>();
                    temporaryList.Add(newPopulatedCell.GetComponent<Populated>());
                    Destroy(cell);
                }
                else if (cell.TryGetComponent(out Populated populatedCell))
                {
                    if (!populatedCell.isDying)
                        return;
                    
                    GameObject newEmptyCell = SetUpCell(emptyCellPrefab, cell.positionInGrid.x, cell.positionInGrid.y);
                    grid[cell.positionInGrid.x, cell.positionInGrid.y] = newEmptyCell.GetComponent<Empty>();
                    Destroy(cell);
                }
            }

            populatedCells = new List<Populated>(temporaryList);
        }
    }
}
