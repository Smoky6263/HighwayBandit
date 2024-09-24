using TMPro;
using UnityEngine;

public class CoinsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI w_textMeshProUGUI, y_textMeshProUGUI;


    public void Start()
    {
        PlayerWallet.OnCoinChangeEvent += SetCoinUI;
        
        w_textMeshProUGUI.text = $"X 0";
        y_textMeshProUGUI.text = $"X 0";
    }

    private void SetCoinUI(int coin)
    {
        w_textMeshProUGUI.text = $"X {coin}";
        y_textMeshProUGUI.text = $"X {coin}";
    }

    private void OnDestroy()
    {
        PlayerWallet.OnCoinChangeEvent -= SetCoinUI;
    }
    private void OnDisable()
    {
        PlayerWallet.OnCoinChangeEvent -= SetCoinUI;        
    }
    private void OnApplicationQuit()
    {
        PlayerWallet.OnCoinChangeEvent -= SetCoinUI;
    }
}
