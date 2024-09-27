using Supercyan.FreeSample;
using UnityEngine;

public class PlayerOnGroundDefeat : MonoBehaviour
{
    private GameObject _gameOverPanel;


    public void Init(GameObject gameOverPanel)
    {
        _gameOverPanel = gameOverPanel;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<SimpleSampleCharacterControl>() != null)
        {
            SimpleSampleCharacterControl playerController = collision.transform.GetComponent<SimpleSampleCharacterControl>();
            Animator playerAnimator = collision.transform.GetComponent<Animator>();
            GameOver(playerController, playerAnimator);
            playerController.transform.SetParent(transform);
            Camera.main.transform.SetParent(null);
            Camera.main.GetComponent<CameraScript>().ControlMode = CameraControllMode.GameOver;
            Camera.main.GetComponent<CameraScript>().ResetCamera();
        }
    }

    private void GameOver(SimpleSampleCharacterControl playerController, Animator playerAnimator)
    {
        playerController.enabled = false;
        playerAnimator.SetFloat("MoveSpeed", 0f);
        playerAnimator.SetBool("Grounded", true);

        LevelFinisher.Defeat();
        _gameOverPanel.SetActive(true);
    }

    
}
