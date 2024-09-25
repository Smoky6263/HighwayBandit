using UnityEngine.SceneManagement;
using UnityEngine;

public class RestartLevel : MonoBehaviour
{

    public void RestartCurrentLevel()
    {
        Scene _currentScne = SceneManager.GetActiveScene();
        SceneManager.LoadScene(_currentScne.name);
    }
}
