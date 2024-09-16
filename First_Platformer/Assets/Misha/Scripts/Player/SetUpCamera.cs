using UnityEngine;

public class SetUpCamera : MonoBehaviour
{
    private ParentingPlayerToObjects _parentingPlayerToObjects;
    private Camera _camera;
    private void Awake()
    {
        _camera = Camera.main.GetComponent<Camera>();

        Transform player = GetComponent<Transform>();
        _camera.GetComponent<CameraScript>().Init(player);

        _parentingPlayerToObjects = GetComponent<ParentingPlayerToObjects>();
        _parentingPlayerToObjects.Init(_camera);
    }
}
