using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameOfLife
{
    public class RestartButton : MonoBehaviour
    {
        public void OnClick()
        {
            SceneManager.LoadScene("MainGame");
        }
    }
}
