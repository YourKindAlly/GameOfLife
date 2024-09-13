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
            
            float mapSizeInUnitsX = GameData.XAxis * grid.CellSize;
            float mapSizeInUnitsY = GameData.YAxis * grid.CellSize;
            currentCamera.transform.position = new Vector3(mapSizeInUnitsX * 0.5f, mapSizeInUnitsY * 0.5f, -10);
        }
        
        public void ChangeCameraSize()
        {
            float changeCameraSize = Input.GetAxis("Mouse ScrollWheel");
            
            currentCamera.orthographicSize = Mathf.Clamp(currentCamera.orthographicSize + changeCameraSize, minSize, maxSize);
        }
    }
}
