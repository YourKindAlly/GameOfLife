using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

namespace GameOfLife.Grid
{
    public class GameGrid : MonoBehaviour
    {
        private int generationTicks;
        [SerializeField] private TMP_Text generationText;
        [SerializeField] private float tickTime = 1;
        [SerializeField] private float tickInterval = 0.1f;
        
        [SerializeField] private int mapSize = 20;
        [SerializeField] private float cellSize;
        private GridCell[,] grid;
        
        private Dictionary<Inhabited, GameObject> inhabitedCells = new();

        [SerializeField] private GameObject inhabitedPrefab;
        
        private Camera mainCamera;
        [SerializeField] private float minCameraSize = 3;
        [SerializeField] private float maxCameraSize = 7;

        private void Start()
        {
            grid = new GridCell[mapSize, mapSize];
            
            SetUpCamera();
            GenerateGrid();
            StartCoroutine(GenerationTick());
        }

        private void SetUpCamera()
        {
            mainCamera = Camera.main;
            
            float mapSizeInUnits = mapSize * cellSize;
            mainCamera.transform.position = new Vector3(mapSizeInUnits * 0.5f, mapSizeInUnits * 0.5f, -10);
        }

        private GameObject SetUpCell(int x, int y)
        {
            GameObject cellObject = Instantiate(inhabitedPrefab, transform);

            cellObject.name = $"Inhabitable {x}, {y}";
            cellObject.transform.position = new Vector3(x, y) * cellSize;
            cellObject.transform.localScale = new Vector3(cellSize, cellSize) * 0.9f;
            
            return cellObject;
        }

        private void GenerateGrid()
        {
            for (int y = 0; y < mapSize; y++)
            {
                for (int x = 0; x < mapSize; x++)
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
        
        private IEnumerator GenerationTick()
        {
            while (true)
            {
                yield return new WaitForSeconds(tickTime);

                generationText.text = $"Generation {++generationTicks}";
                CheckGridForNextTick();
                UpdateGridCells();
            }
        }

        private void CheckGridForNextTick()
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

        private void UpdateGridCells()
        {
            foreach (GridCell gridCell in grid)
            {
                if (gridCell is Empty { IsInhabitable: true })
                {
                    Inhabited newCell = new(gridCell.PositionInGrid);
                    grid[gridCell.PositionInGrid.x, gridCell.PositionInGrid.y] = newCell;
                    
                    GameObject newGameObject = SetUpCell(gridCell.PositionInGrid.x, gridCell.PositionInGrid.y);
                    newGameObject.transform.position = new Vector2(gridCell.PositionInGrid.x, gridCell.PositionInGrid.y) * cellSize;
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
        
        private void Update()
        {
            ChangeCameraSize();
            ChangeTickTime();
        }

        private void ChangeCameraSize()
        {
            float changeCameraSize = Input.GetAxis("Mouse ScrollWheel");
            
            mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize + changeCameraSize, minCameraSize, maxCameraSize);
        }

        private void ChangeTickTime()
        {
            if (Input.GetKeyDown(KeyCode.D))
                tickTime = Mathf.Clamp(tickTime + tickInterval, 0.5f, 3);
            else if (Input.GetKeyDown(KeyCode.A))
                tickTime = Mathf.Clamp(tickTime - tickInterval, 0.5f, 3);
        }
    }
}
