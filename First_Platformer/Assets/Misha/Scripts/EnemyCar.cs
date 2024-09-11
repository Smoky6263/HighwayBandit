using UnityEngine;

public class EnemyCar : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _speed;

    private void FixedUpdate()
    {
        Vector3 targetPosition = new Vector3(_player.position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, _speed);
    }
}
