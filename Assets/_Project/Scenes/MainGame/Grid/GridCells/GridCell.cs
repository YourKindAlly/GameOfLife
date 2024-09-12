using UnityEngine;

namespace GameOfLife.Grid
{
    public class GridCell : MonoBehaviour
    {
        [HideInInspector] public Vector2Int positionInGrid;
        
        public SpriteRenderer Sprite { get; private set; }

        public CellType CurrentType { get; set; }

        public bool IsBorn { get; private set; }
        public bool IsDying { get; private set; }

        private void Awake()
        {
            Sprite = gameObject.GetComponent<SpriteRenderer>();
        }

        public void ChangeType()
        {
            if (CurrentType == CellType.Empty)
            {
                CurrentType = CellType.Inhabited;
                Sprite.enabled = true;
            }
            else
            {
                CurrentType = CellType.Inhabited;
                Sprite.enabled = false;
            }
            
            ResetStates();
        }

        public void EnableBornState()
        {
            if (CurrentType == CellType.Empty) 
                return;
            
            IsBorn = true;
        }

        public void EnableDyingState()
        {
            if (CurrentType == CellType.Inhabited)
                return;
            
            IsDying = true;
        }

        private void ResetStates()
        {
            IsBorn = false;
            IsDying = false;
        }

        public void ChangeSprite()
        {
            Sprite.enabled = !Sprite.enabled;
        }
    }
}
