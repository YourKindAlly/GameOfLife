using UnityEngine;
using TMPro;

namespace GameOfLife
{
    [RequireComponent(typeof(TMP_Text))]
    public class InputWarning : MonoBehaviour
    {
        private TMP_Text textComponent;
        
        private void Start()
        {
            textComponent = GetComponent<TMP_Text>();
        }

        public void SetText(string text)
        {
            textComponent.text = text;
        }
    }
}
