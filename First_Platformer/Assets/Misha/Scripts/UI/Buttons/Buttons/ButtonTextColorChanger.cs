using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]
public class ButtonTextColorChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] private AudioClip _buttonHighlight;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color highlightedColor = Color.white;
    
    private AudioSource _buttonSource;
    private TextMeshProUGUI _buttonText; 

    private void Start()
    {
        _buttonSource = GetComponent<AudioSource>();
        _buttonText = GetComponent<TextMeshProUGUI>();
        normalColor = _buttonText.color;

        _buttonText.color = normalColor;
    }

    public void OnPointerEnter(PointerEventData eventData) 
    {
        _buttonSource.PlayOneShot(_buttonHighlight);
        _buttonText.color = highlightedColor;
    } 

    public void OnPointerExit(PointerEventData eventData) => _buttonText.color = normalColor;
}