using UnityEngine;

public class ParentingPlayerToObjects : MonoBehaviour
{
    private Camera _camera;
    private CameraScript _cameraScript;

    public void Init(Camera camera)
    {
        _camera = camera;
        _cameraScript = camera.GetComponent<CameraScript>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        SetParent(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        ResetParent();
    }

    private void SetParent(Collision collision)
    {
        if (collision.transform.GetComponentInParent<CivilianCar>() != null)
        {
            transform.SetParent(collision.transform);
            _camera.transform.SetParent(collision.transform);
        }
    }

    private void ResetParent()
    {     
        _camera.transform.SetParent(null);
        transform.SetParent(null);
    }
}
