using UnityEngine;

public class SetUpCamera : MonoBehaviour
{
    private Camera _camera;
    public Camera Camera { get { return _camera; } }
    private void Awake()
    {
        _camera = Camera.main.GetComponent<Camera>();

        Transform player = GetComponent<Transform>();
        _camera.GetComponent<CameraScript>().Init(player);

    }
}