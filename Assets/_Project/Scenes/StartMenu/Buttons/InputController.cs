using UnityEngine;
using System;
using TMPro;

namespace GameOfLife
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] private TMP_Text warningText;
        public int InputValue { get; private set; }
        private const int MinInputValue = 50;
        private const int MaxInputValue = 200;
        public bool IsValid { get; private set; }

        public void OnValueChanged(string value)
        {
            warningText.gameObject.SetActive(false);
        }
        
        public void OnEndEdit(string value)
        {
            try
            {
                InputValue = int.Parse(value);
                if (InputValue is < MinInputValue or > MaxInputValue)
                {
                    warningText.gameObject.SetActive(true);
                    warningText.SetText("Please enter a number between {0} and {1}", MinInputValue, MaxInputValue);
                }
                else
                {
                    IsValid = true;
                }

            }
            catch (FormatException)
            {
                warningText.gameObject.SetActive(true);
                warningText.SetText("Please enter a valid positive whole number");
            }
        }
    }
}
