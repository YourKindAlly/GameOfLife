using UnityEngine;
using System.Collections;
using GameOfLife.Grid;

namespace GameOfLife.Core
{
    public class TickManager : MonoBehaviour
    {
        [SerializeField] private GameGrid grid;
        [SerializeField] private TextController generationText;
        
        [SerializeField] private float tickInterval = 1;
        [SerializeField] private float tickStep = 0.1f;
        
        private void Start()
        {
            StartCoroutine(GenerationTick());
        }

        private IEnumerator GenerationTick()
        {
            while (true)
            {
                yield return new WaitForSeconds(tickInterval);
                
                generationText.SetText();
                grid.CheckGridForNextTick();
                grid.UpdateGridCells();
            }
        }

        private void Update()
        {
            ChangeTickTime();
        }
        
        private void ChangeTickTime()
        {
            if (Input.GetKeyDown(KeyCode.D))
                tickInterval = Mathf.Clamp(tickInterval + tickStep, 0.5f, 3);
            else if (Input.GetKeyDown(KeyCode.A))
                tickInterval = Mathf.Clamp(tickInterval - tickStep, 0.5f, 3);
        }
    }
}
