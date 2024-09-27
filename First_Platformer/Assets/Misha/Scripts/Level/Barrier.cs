using UnityEngine;

public class Barrier : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerOnJump>() != null)
        {
            other.transform.SetParent(null);
            Camera.main.GetComponent<CameraScript>().ControlMode = CameraControllMode.GameOver;
            Camera.main.GetComponent<CameraScript>().ResetCamera();
        }
    }
}
