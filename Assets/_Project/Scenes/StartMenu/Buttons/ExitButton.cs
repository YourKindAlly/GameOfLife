using UnityEngine;

namespace GameOfLife
{
    public class ExitButton : MonoBehaviour
    {
        public void OnClick()
        {
            Application.Quit();
        }
    }
}
