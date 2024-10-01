using Supercyan.FreeSample;
using UnityEngine;

public class PlayerOnGameOver : MonoBehaviour
{
    public void OnGameOver()
    {
        SimpleSampleCharacterControl controller = GetComponent<SimpleSampleCharacterControl>();
        Animator animator = GetComponent<Animator>();
        Camera camera = GetComponent<SetUpCamera>().Camera;

        camera.transform.SetParent(null);
        camera.GetComponent<CameraScript>().ControlMode = CameraControllMode.GameOver;
        camera.GetComponent<CameraScript>().ResetCamera();

        controller.enabled = false;
        animator.SetFloat("MoveSpeed", 0f);
        animator.SetBool("Grounded", true);
    }
}
