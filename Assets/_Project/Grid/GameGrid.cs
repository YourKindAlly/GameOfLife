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
        
        private List<GameObject> populatedCells = new();

        [SerializeField] private GameObject populatedPrefab;

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
                    
                }
            }
        }

        private void UpdateGridCells()
        {
            
        }
    }
}
