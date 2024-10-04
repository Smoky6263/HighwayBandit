using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEntryPoint : MonoBehaviour
{
    [SerializeField] private string _sceneName;
    [SerializeField] private Slider _loadingSlider;

    private void Start() => StartCoroutine(LoadingGameCoroutine(_sceneName));
    private IEnumerator LoadingGameCoroutine(string sceneName)
    {
        Application.targetFrameRate = 60;
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            _loadingSlider.value = progressValue;

            if (asyncOperation.progress >= 0.9f)
            {
                yield return new WaitForSeconds(5f);
                asyncOperation.allowSceneActivation = true;
            }
        }
        yield return null;
    }
}
