using System;

public static class PlayerWallet
{
    public static event Action<int> OnCoinChangeEvent;

    private static int _coinsCount = 0;

    public static void AddCoin()
    {
        _coinsCount++;
        OnCoinChangeEvent?.Invoke(_coinsCount);
    }
}
