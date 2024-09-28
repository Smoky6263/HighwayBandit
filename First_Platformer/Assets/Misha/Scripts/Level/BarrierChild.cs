using UnityEngine;

public class BarrierChild : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake() => _audioSource = GetComponent<AudioSource>();
    private void OnTriggerEnter(Collider other)
    {

        if(other.GetComponent<PlayerOnJump>() != null)
        {
            other.transform.SetParent(null);
            Camera.main.GetComponent<CameraScript>().ControlMode = CameraControllMode.GameOver;
            Camera.main.GetComponent<CameraScript>().ResetCamera();
            //_audioSource
        }
    }
}
