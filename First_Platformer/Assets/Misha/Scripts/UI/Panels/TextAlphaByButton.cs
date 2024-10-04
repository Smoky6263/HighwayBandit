using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextAlphaByButton : MonoBehaviour
{
    [SerializeField] private Image _button;
    [SerializeField] private TextMeshProUGUI _text;
    private void Start()
    {
        StartCoroutine(SetTextAlpha());
    }

    private IEnumerator SetTextAlpha()
    {
        float time = 2f;

        while (time > 0)
        {
            yield return new WaitForFixedUpdate();
            
            time -= Time.deltaTime;
            
            Color buttonColor = _button.color;
            Color textColor = _text.color;
            textColor.a = buttonColor.a;

            _text.color = textColor;
        }
        yield break;
    }

    private void OnDisable() => StopAllCoroutines();
    private void OnDestroy() => StopAllCoroutines();
}
