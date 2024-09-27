
using System;

public static class LevelFinisher
{
    public static event Action OnVictoryCarSpawnEvent;
    public static event Action PlayerDefeatEvent;

    public static void LevelPassed() => OnVictoryCarSpawnEvent?.Invoke();
    public static void Defeat() => PlayerDefeatEvent?.Invoke();

}
