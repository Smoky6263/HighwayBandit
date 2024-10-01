using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private Slider _loadingSlider;
    [SerializeField] private string _sceneName;
    public void Load() => StartCoroutine(LoadingGameCoroutine(_sceneName));

    private IEnumerator LoadingGameCoroutine(string sceneName)
    {
        _loadingScreen.SetActive(true);

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            _loadingSlider.value = progressValue;

            if(asyncOperation.progress >= 0.9f)
            {
                yield return new WaitForSeconds(2f);
                asyncOperation.allowSceneActivation = true;
            }
        }
        yield return null;
    }
}
