using GameOfLife.Core;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace GameOfLife
{
    public class SelectInput : MonoBehaviour
    {
        [SerializeField] private string sceneName;
        [SerializeField] private InputController xInput;
        [SerializeField] private InputController yInput;
        
        public void OnClick()
        {
            if (!CheckInputValidity())
                return;

            GameData.XAxis = xInput.InputValue;
            GameData.YAxis = yInput.InputValue;

            SceneManager.LoadScene(sceneName);
        }

        private bool CheckInputValidity()
        {
            return xInput.IsValid && yInput.IsValid;
        }
    }
}
