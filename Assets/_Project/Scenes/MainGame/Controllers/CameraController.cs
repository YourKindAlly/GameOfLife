using UnityEngine;
using GameOfLife.Grid;

namespace GameOfLife.Core
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private GameGrid grid;
        
        private Camera currentCamera;

        [SerializeField] private float minSize = 3;
        [SerializeField] private float maxSize = 7;

        private void Start()
        {
            SetUp();
        }

        private void SetUp()
        {
            currentCamera = GetComponent<Camera>();
            
            float mapSizeInUnits = grid.MapSize * grid.CellSize;
            currentCamera.transform.position = new Vector3(mapSizeInUnits * 0.5f, mapSizeInUnits * 0.5f, -10);
        }

        private void Update()
        {
            ChangeCameraSize();
        }
        
        private void ChangeCameraSize()
        {
            float changeCameraSize = Input.GetAxis("Mouse ScrollWheel");
            
            currentCamera.orthographicSize = Mathf.Clamp(currentCamera.orthographicSize + changeCameraSize, minSize, maxSize);
        }
    }
}
