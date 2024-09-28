using System.Collections;
using UnityEngine;

public enum CameraControllMode { Player, GameOver }
public class CameraScript : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _cameraHeight;
    [SerializeField] private float _cameraOffset;
    [SerializeField] private float _cameraPositionSpeed;
    [SerializeField] private float _cameraRotationSpeed;

    [Header("Скорость камеры, с которой она будет двигаться вперед, когда игрок проиграет")]
    [SerializeField] private float _gameOverCameraSpeed;

    private CameraControllMode _controlMode;

    public CameraControllMode ControlMode { get { return _controlMode; } set { _controlMode = value; } }

    public void Init(Transform player)
    {
        _player = player;
        _controlMode = CameraControllMode.Player;
    }

    private void FixedUpdate()
    {
        switch (_controlMode)
        {
            case CameraControllMode.Player:
                CameraPosition();
                break;
            
            case CameraControllMode.GameOver:
                OnGameOverMove();
                break;

            default:
                Debug.LogWarning($"Некорректный ControlMode: {_controlMode}");
                break;
        }
    }

    private void OnGameOverMove() => transform.position += Vector3.forward * _gameOverCameraSpeed * Time.deltaTime;

    private void CameraPosition()
    {
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = new Vector3(_player.transform.position.x, _player.transform.position.y + _cameraHeight, _player.transform.position.z - _cameraOffset);

        transform.position = Vector3.Lerp(currentPosition, targetPosition, _cameraPositionSpeed);
        
        CameraRotation();
    }
    private void CameraRotation()
    {
        Vector3 direction = _player.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _cameraRotationSpeed);
    }

    public void ResetCamera() => StartCoroutine(ResetCameraCoroutine());

    private IEnumerator ResetCameraCoroutine()
    {
        transform.parent = null;
        float time = 2f;
        while (time > 0f)
        {
            yield return new WaitForFixedUpdate();
            time -= Time.deltaTime;

            transform.position = Vector3.Lerp(transform.position, new Vector3(0f, 12f, transform.position.z), _cameraPositionSpeed);
            
            Vector3 direction = new Vector3(0f, -7.51f, 7.53f);
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _cameraPositionSpeed);
        }
        yield break;
    }

    private void OnDisable() => StopCoroutine(ResetCameraCoroutine());
    private void OnDestroy() => StopCoroutine(ResetCameraCoroutine());
    private void OnApplicationQuit() => StopCoroutine(ResetCameraCoroutine());
}
