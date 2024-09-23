using UnityEngine;

public class EnemyCar : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _offset;
    [SerializeField, Range(0f,1f)] private float _speed;
    
    public void Init(Transform player)
    {
        _player = player;
    }
    
    private void FixedUpdate()
    {
        MoveCar();
    }
    private void MoveCar()
    {
        Vector3 targetPosition = new Vector3(_player.position.x, transform.position.y, _player.position.z + _offset);
        transform.position = Vector3.Lerp(transform.position, targetPosition, _speed);
    }
}
