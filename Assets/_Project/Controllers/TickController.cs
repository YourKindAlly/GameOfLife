using UnityEngine;
using System.Collections;
using GameOfLife.Grid;

namespace GameOfLife.Core
{
    public class TickController : MonoBehaviour
    {
        [SerializeField] private float tickInterval = 1;
        [SerializeField] private float tickStep = 0.1f;
        [SerializeField] private float minTick = 0.1f;
        [SerializeField] private float maxTick = 3;

        public IEnumerator GenerationTick(TextController text, GameGrid grid)
        {
            while (true)
            {
                yield return new WaitForSeconds(tickInterval);
                
                text.SetText();
                grid.CheckGridForNextTick();
                grid.UpdateGridCells();
            }
        }
        
        public void ChangeTickTime()
        {
            if (Input.GetKeyDown(KeyCode.D))
                tickInterval = Mathf.Clamp(tickInterval + tickStep, minTick, maxTick);
            else if (Input.GetKeyDown(KeyCode.A))
                tickInterval = Mathf.Clamp(tickInterval - tickStep, minTick, maxTick);
        }
    }
}
