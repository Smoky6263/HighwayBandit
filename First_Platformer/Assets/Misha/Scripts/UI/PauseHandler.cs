using System;

public class PauseHandler
{
    public event Action OnPauseEvent;

    public void OnPause() => OnPauseEvent?.Invoke();
}
