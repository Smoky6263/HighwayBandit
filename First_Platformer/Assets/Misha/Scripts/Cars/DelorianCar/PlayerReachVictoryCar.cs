using Supercyan.FreeSample;
using UnityEngine;

public class PlayerReachVictoryCar : MonoBehaviour
{
    private GameObject _levelPassedPanel;

    public void Init(GameObject levelPassedPanel)
    {
        _levelPassedPanel = levelPassedPanel;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<PlayerController>() != null)
        {
            PlayerController playerController = collision.transform.GetComponent<PlayerController>();
            Animator playerAnimator = collision.transform.GetComponent<Animator>();
            playerController.transform.SetParent(transform);
            LevelPassed(playerController, playerAnimator);
        }
    }

    private void LevelPassed(PlayerController playerController, Animator playerAnimator)
    {
        playerController.enabled = false;
        playerAnimator.SetFloat("MoveSpeed", 0f);
        playerAnimator.SetBool("Grounded", true);
        playerAnimator.SetTrigger("Win");

        _levelPassedPanel.SetActive(true);
    }
}
