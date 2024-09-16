using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameOfLife
{
    public class StartNew : MonoBehaviour
    {
        public void OnClick()
        {
            SceneManager.LoadScene("Start");
        }
    }
}
