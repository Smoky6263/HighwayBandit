using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class ButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private AudioClip b_Click;
    [SerializeField] private AudioClip b_Highlight;
    [SerializeField] private TextMeshProUGUI _buttonText;

    [SerializeField]
    private Color _highlightedColor = Color.white;
    private Color _currentColor;

    [SerializeField, Range(0f, 100f)] private float f_buttonScalePercantage;
    [SerializeField, Range(0f, 1f)] private float _scaleTime;

    protected Button _button;
    private AudioSource _audioSource;

    private Vector3 _currentScale;
    private Vector3 _targetScale;

    protected virtual void Awake()
    {
        _button = GetComponent<Button>();
        _audioSource = GetComponent<AudioSource>();
        _button.onClick.AddListener(PlaySound);

        if(_buttonText != null)
        {
            _currentColor = _buttonText.color;
            _buttonText.color = _currentColor;
        }

        _currentScale = new Vector3(transform.localScale.x, transform.localScale.y);
        _targetScale = transform.localScale * (1 + (f_buttonScalePercantage / 100f));
    }
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        
    }

    private void PlaySound() => _audioSource.PlayOneShot(b_Click);
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        LeanTween.scale(gameObject, _targetScale, _scaleTime).setEase(LeanTweenType.easeInOutBack);
        _audioSource.PlayOneShot(b_Highlight);

        if (_buttonText != null)
            _buttonText.color = _highlightedColor;
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (_buttonText != null)
            _buttonText.color = _currentColor;

        LeanTween.scale(gameObject, _currentScale, _scaleTime).setEase(LeanTweenType.easeInOutBack);
    }

    private void OnEnable() => _button.onClick.AddListener(PlaySound);
    private void OnDisable() => _button.onClick.RemoveListener(PlaySound);
    private void OnDestroy() => _button.onClick.RemoveListener(PlaySound);

}
