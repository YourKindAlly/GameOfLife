using UnityEngine;
using TMPro;

namespace GameOfLife.Core
{
    [RequireComponent(typeof(TMP_Text))]
    public class TextController : MonoBehaviour
    {
        private TMP_Text generationText;
        private int generationsTicked;

        private void Start()
        {
            generationText = GetComponent<TMP_Text>();   
        }

        public void SetText()
        {
            generationText.text = $"Generation {++generationsTicked}";
        }
    }
}
