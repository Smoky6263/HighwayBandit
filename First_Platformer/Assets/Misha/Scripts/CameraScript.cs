using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _cameraHeight;
    [SerializeField] private float _cameraSpeed;


    private void FixedUpdate()
    {
        CameraPosition();
        CameraRotation();
    }

    private void CameraPosition()
    {
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = new Vector3(_player.transform.position.x, _player.transform.position.y + _cameraHeight, transform.position.z);

        transform.position = Vector3.Lerp(currentPosition, targetPosition, _cameraSpeed);
    }
    private void CameraRotation()
    {
        Quaternion currentRotation = transform.rotation;
        Quaternion targetRotation = _player.transform.rotation;

        transform.LookAt(_player.transform.position);
    }
}
