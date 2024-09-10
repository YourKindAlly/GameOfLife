using UnityEngine;
using System;
using TMPro;

namespace GameOfLife
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] private TMP_Text warningText;
        public int InputValue { get; private set; }

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
                if (InputValue is < 10 or > 200)
                {
                    warningText.SetText("Please enter a number between 10 and 200");
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
