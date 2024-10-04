using UnityEngine;

public class Barrier : MonoBehaviour
{
    private RandomElementsRespawner _barrierRespawner;
    private Transform _respawnPosition;
    private float _respawnDistance;
    private float _speed;

    private Vector3 _lastPosition;
    private float _totalDistance;


    public void Init(Transform player, Transform respawnPosition, float respawnDistance, float speed)
    {
        _speed = speed;
        _respawnPosition = respawnPosition;
        _respawnDistance = respawnDistance;
        _lastPosition = transform.position;
        _totalDistance = 0f;
        _barrierRespawner = GetComponentInChildren<RandomElementsRespawner>();
    }
    public void ChangeSpeed(float value) => _speed = value;

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);

        float distance = Vector3.Distance(transform.position, _lastPosition);
        _totalDistance += distance;
        _lastPosition = transform.position;

        if(_totalDistance > _respawnDistance) 
        {
            transform.position = _respawnPosition.position;
            _barrierRespawner.ResetElements();
            _totalDistance = 0f;
        }
    }
}
