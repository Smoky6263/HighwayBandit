using System.Collections;
using TMPro;
using UnityEngine;

public class GameInterface : MonoBehaviour
{
    [Header("Картинка с BG")]
    [SerializeField] private RectTransform _BG;

    [Header("Текст таймера в начале игры")]
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private float _timerTime;
    [SerializeField] private float _timerScale;

    private void Start()
    {
        _timerText.gameObject.SetActive(false);
        StartCoroutine(InitTimerCoroutine());
    }

    private IEnumerator InitTimerCoroutine()
    {
        _BG.gameObject.SetActive(true);
        LeanTween.alpha(_BG, 0f, _timerTime).setEaseInQuint();
        yield return new WaitForSeconds(2f);
        _timerText.gameObject.SetActive(true);
        _timerText.text = "3";

        RectTransform rectTransform = _timerText.GetComponent<RectTransform>();


        LeanTween.scale(rectTransform, new Vector3(_timerScale, _timerScale, _timerScale), _timerTime).setEaseInQuint();
        yield return new WaitForSeconds(_timerTime);

        rectTransform.localScale = Vector3.one;
        _timerText.text = "2";

        LeanTween.scale(rectTransform, new Vector3(_timerScale, _timerScale, _timerScale), _timerTime).setEaseInQuint();
        yield return new WaitForSeconds(_timerTime);

        rectTransform.localScale = Vector3.one;
        _timerText.text = "1";

        LeanTween.scale(rectTransform, new Vector3(_timerScale, _timerScale, _timerScale), _timerTime).setEaseInQuint();
        yield return new WaitForSeconds(_timerTime);

        rectTransform.localScale = Vector3.one;
        _timerText.text = "GO!";

        LeanTween.scale(rectTransform, new Vector3(_timerScale, _timerScale, _timerScale), _timerTime).setEaseInQuint();
        yield return new WaitForSeconds(_timerTime);

        _timerText.gameObject.SetActive(false);
        _BG.gameObject.SetActive(false);
        yield break;
    }
}
