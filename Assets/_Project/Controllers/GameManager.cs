using UnityEngine;
using GameOfLife.Grid;

namespace GameOfLife.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameGrid grid;
        [SerializeField] private TextController textController;
        [SerializeField] private TickController tickController;
        [SerializeField] private CameraController cameraController;

        private void Start()
        {
            StartCoroutine(tickController.GenerationTick(textController, grid));
        }

        private void Update()
        {
            cameraController.ChangeCameraSize();
            tickController.ChangeTickTime();
        }
    }
}
