using System;

public static class PlayerWallet
{
    public static event Action<int> OnCoinChangeEvent;

    private static int _coinsCount = 0;
    private static int _coinsCountToEndLevel = 0;
    
    public static int CoinsCount { get { return _coinsCount; } set { _coinsCount = value; } }
    public static int CoinsCountToEndLevel { get { return _coinsCountToEndLevel; } set { _coinsCountToEndLevel = value; } }
    public static void AddCoin()
    {
        _coinsCount++;
        OnCoinChangeEvent?.Invoke(_coinsCount);

        if (_coinsCount == _coinsCountToEndLevel)
            LevelFinisher.LevelPassed();
    }
}
