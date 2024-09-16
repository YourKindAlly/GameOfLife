using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameOfLife
{
    public class StartNewButton : MonoBehaviour
    {
        public void OnClick()
        {
            SceneManager.LoadScene("Start");
        }
    }
}
