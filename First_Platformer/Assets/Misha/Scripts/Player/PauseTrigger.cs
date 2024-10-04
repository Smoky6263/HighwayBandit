using UnityEngine;

public class PauseTrigger : MonoBehaviour
{
    private PauseHandler _pauseHandler;

    public void Bind(PauseHandler pauseHandler)
    {
        _pauseHandler = pauseHandler;
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape)) 
            _pauseHandler.OnPause();
    }
}
