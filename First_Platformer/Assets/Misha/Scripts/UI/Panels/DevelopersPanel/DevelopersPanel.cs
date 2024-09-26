using UnityEngine;
using UnityEngine.UI;

public class DevelopersPanel : MonoBehaviour
{
    [SerializeField] private Image _fadeBG;
    [SerializeField] private float t_fadeTime;
    [SerializeField] private float panel_currentAlphaValue = 0.7843137f;

    [SerializeField] private CanvasGroup _fadeImage;
    [SerializeField] private float y_fadeImageIn = 0f, y_fadeImageOut = 0f;


    private void OnEnable() => DoFadeOut();

    private void DoFadeOut()
    {
        LeanTween.value(_fadeBG.gameObject, 0f, panel_currentAlphaValue, t_fadeTime).setOnUpdate((float value) =>
        {
            Color newColor = _fadeBG.color;
            newColor.a = value;
            _fadeBG.color = newColor;
        }).setEase(LeanTweenType.easeOutBack);

        _fadeImage.alpha = 0f;
        LeanTween.alphaCanvas(_fadeImage, 1f, t_fadeTime).setEase(LeanTweenType.easeOutBack);

        LeanTween.moveLocalY(_fadeImage.gameObject, y_fadeImageIn, t_fadeTime).setEase(LeanTweenType.easeOutBack);
    }

    public void DoFadeIn()
    {
        LeanTween.value(_fadeBG.gameObject, panel_currentAlphaValue, 0f, t_fadeTime).setOnUpdate((float value) =>
        {
            Color newColor = _fadeBG.color;
            newColor.a = value;
            _fadeBG.color = newColor;
        }).setEase(LeanTweenType.easeOutBack);

        LeanTween.alphaCanvas(_fadeImage, 0f, t_fadeTime).setEase(LeanTweenType.easeOutBack);

        LeanTween.moveLocalY(_fadeImage.gameObject, y_fadeImageOut, t_fadeTime).setEase(LeanTweenType.easeOutBack).setOnComplete(() => gameObject.SetActive(false));
    }

}
