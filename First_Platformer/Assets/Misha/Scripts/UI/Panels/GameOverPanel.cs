using System.Collections;
using TMPro;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    private CanvasGroup _group;

    [SerializeField] private float _alphaTime;

    [Header("Main Text")]
    [SerializeField] private RectTransform _mainText;
    [SerializeField] private float _mainTextDelay, _mainTextTime;
    [SerializeField] private float _mainTextFrom, _mainTextTo;

    [Header("Buttons")]
    [SerializeField] private RectTransform[] _buttons;
    [SerializeField] private float _buttonTime;
    [SerializeField] private float[] _buttonDelay;
    [SerializeField] private float[] _buttonFrom, _buttonTo;

    [Header("Элемент BG для перехода в темный экран")]
    [SerializeField] private RectTransform _BG;

    private void Awake()
    {
        _group = GetComponent<CanvasGroup>();
        _group.alpha = 0f;
        _BG.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        FadeOut();
    }

    private void FadeOut()
    {
        LeanTween.alphaCanvas(_group, 1f, _alphaTime).setEaseOutSine();

        Color textColor = _mainText.GetComponent<TextMeshProUGUI>().color;
        LeanTween.value(gameObject, textColor.a, 1f, _mainTextDelay)
                .setOnUpdate((float alphaValue) =>
                {
                    textColor.a = alphaValue;
                    _mainText.GetComponent<TextMeshProUGUI>().color = textColor;
                }).setDelay(_mainTextDelay).setEaseOutSine();

        LeanTween.moveY(_mainText, _mainTextTo, _mainTextTime).setDelay(_mainTextDelay).setEaseOutSine();
        for (int i = 0; i < _buttons.Length; i++)
        {
            LeanTween.alpha(_buttons[i], 1f, _buttonTime).setDelay(_buttonDelay[i]).setEaseOutSine();
            
            LeanTween.moveY(_buttons[i], _buttonTo[i], _buttonTime).setDelay(_buttonDelay[i]).setEaseOutSine();
        }
    }
    public void FadeInAndRestartLevel() => StartCoroutine(FadeInCoroutine());

    private IEnumerator FadeInCoroutine()
    {
        _BG.gameObject.SetActive(true);
        LeanTween.alpha(_BG, 1f, _alphaTime).setEaseOutSine();
        yield return new WaitForSeconds(_alphaTime);
        GetComponentInChildren<ButtonsEvents>().RestartCurrentLevel();
        yield break;
    }
}
