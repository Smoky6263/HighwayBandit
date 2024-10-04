using UnityEngine;

public class PlayerOnDefeatTrigger : MonoBehaviour
{
    protected GameObject _gameOverPanel;

    public virtual void Init(GameObject gameOverPanel)
    {
        _gameOverPanel = gameOverPanel;
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<PlayerOnGameOver>() != null)
            GameOver(collision);
    }

    protected virtual void GameOver(Collision collision)
    {
        collision.transform.parent = transform;
        collision.transform.GetComponent<PlayerOnGameOver>().OnGameOver();
        LevelFinisher.Defeat();
        _gameOverPanel.SetActive(true);
    }

    
}
