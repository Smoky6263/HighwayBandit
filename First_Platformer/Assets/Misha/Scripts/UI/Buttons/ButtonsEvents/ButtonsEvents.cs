using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

public class ButtonsEvents : MonoBehaviour
{

    public void RestartCurrentLevel()
    {
        Scene _currentScne = SceneManager.GetActiveScene();
        SceneManager.LoadScene(_currentScne.name);
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(StartLoadLevel(sceneName));
    }

    private IEnumerator StartLoadLevel(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

        while(asyncLoad.isDone == false) 
        {
            yield return null;
        }


        yield break;
    }

    public void ApplicationQuit() => Application.Quit();
}
