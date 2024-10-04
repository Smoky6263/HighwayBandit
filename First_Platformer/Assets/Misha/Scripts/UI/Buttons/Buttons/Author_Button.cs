using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

[RequireComponent(typeof(AudioSource))]
public class Author_Button : ButtonScript
{
    [SerializeField] private GameObject _authorPanel;
    [SerializeField] private GameObject[] _buttonsList;
    private bool _buttonClicked = false;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        OnClick();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);

        _authorPanel.SetActive(true);
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);

        if (_buttonClicked == true) return;
        _authorPanel.SetActive(false);
    }
    public void OnClick()
    {
        for (int i = 0; i < _buttonsList.Length; i++)
        {
            if (_buttonsList[i] == this.gameObject) continue;
            _buttonsList[i].SetActive(_buttonClicked);
        }
        
        _buttonClicked = !_buttonClicked;        
    }
}
