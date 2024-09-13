using UnityEngine;

public class SetCamera : MonoBehaviour
{
    private void Awake()
    {
        Transform player = GetComponent<Transform>();
        Camera.main.GetComponent<CameraScript>().Init(player);
    }
}
