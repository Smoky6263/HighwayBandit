using Supercyan.FreeSample;
using System;
using UnityEngine;

public class PlayerOnGameOver : MonoBehaviour
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
            playerController.transform.SetParent(transform);
            GameOver(playerController, playerAnimator);
        }
    }

    private void GameOver(SimpleSampleCharacterControl playerController, Animator playerAnimator)
    {
        playerController.enabled = false;
        playerAnimator.SetFloat("MoveSpeed", 0f);
        playerAnimator.SetBool("Grounded", true);

        _gameOverPanel.SetActive(true);
    }

    
}
