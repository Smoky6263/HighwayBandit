using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonsEvents : MonoBehaviour
{

    public void RestartCurrentLevel()
    {
        Scene _currentScne = SceneManager.GetActiveScene();
        SceneManager.LoadScene(_currentScne.name);
    }

    public void ApplicationQuit() => Application.Quit();
}
