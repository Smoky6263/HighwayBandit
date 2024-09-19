using UnityEngine;

public class EnemyCar : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _timeToStart = 5f;
    [SerializeField] private float _distanceToStartGame;
    
    public void Init(Transform player)
    {
        _player = player;
    }

    private float _speed;
    private void Awake()
    {
        _speed = _distanceToStartGame / _timeToStart;
    }
    
    private void FixedUpdate()
    {
        MoveCar();
    }
    private void MoveCar()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        Vector3 targetPosition = new Vector3(_player.position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, _speed);
    }
}
