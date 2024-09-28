using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : MonoBehaviour
{
    [Header("Картинка BG")]
    [SerializeField] private Image _fadeBG;
    [SerializeField] private float t_fadeTime;


    [Header("Текст с названием игры")]
    [SerializeField] private GameObject _highwayText;
    [SerializeField] private GameObject _banditText;
    [SerializeField] private float x_highwayText = 40f;
    [SerializeField] private float t_highwayText;
    [SerializeField] private float d_highwayText;
    [SerializeField] private float x_banditText = 220f;
    [SerializeField] private float t_banditText;
    [SerializeField] private float d_banditText;

    [Header("Основные кнопки")]
    [SerializeField] private GameObject[] _buttons;
    [SerializeField] private float[] _buttonsDelay;
    [SerializeField] private float _buttonsTargetPosition = 480f;

    [Header("Текст с названием игры")]
    [SerializeField] private GameObject[] _chooseArrow;
    [SerializeField] private float[] _chooseDelay;
    [SerializeField] private float _chooseArrowTargetPosition;

    private void Start()
    {
        DoFadeOut();
        DoNameOfGameOut();
        DoButtonsOut();
        DoChooseArrowOut();
    }


    private void DoFadeOut()
    {
        LeanTween.value(gameObject, _fadeBG.color.a, 0f, t_fadeTime).setOnUpdate((float value) =>
        {
            Color newColor = _fadeBG.color;
            newColor.a = value;
            _fadeBG.color = newColor;
        });
    }
    
    private void DoNameOfGameOut()
    {
        LeanTween.moveLocalX(_highwayText.gameObject, x_highwayText, t_highwayText).setDelay(d_highwayText).setEase(LeanTweenType.easeOutBack);
        LeanTween.moveLocalX(_banditText.gameObject, x_banditText, t_banditText).setDelay(d_banditText).setEase(LeanTweenType.easeOutBack);
    }
    private void DoButtonsOut()
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            LeanTween.moveLocalX(_buttons[i].gameObject, _buttonsTargetPosition, t_highwayText).setDelay(_buttonsDelay[i]).setEase(LeanTweenType.easeOutBack);
        }
    }

    private void DoChooseArrowOut()
    {
        for (int i = 0; i < _chooseArrow.Length; i++)
        {
            LeanTween.moveLocalY(_chooseArrow[i].gameObject, _chooseArrowTargetPosition, t_highwayText).setDelay(_chooseDelay[i]).setEase(LeanTweenType.easeOutBack);
        }
    }

}
