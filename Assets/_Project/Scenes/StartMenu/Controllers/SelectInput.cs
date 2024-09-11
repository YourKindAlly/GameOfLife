using UnityEngine;
using TMPro;

namespace GameOfLife
{
    public class SelectInput : MonoBehaviour
    {
        [SerializeField] private InputController xInput;
        [SerializeField] private InputController yInput;
        
        public void OnClick()
        {
            if (!CheckInputValidity())
                return;
        }

        private bool CheckInputValidity()
        {
            return xInput.IsValid && yInput.IsValid;
        }
    }
}
