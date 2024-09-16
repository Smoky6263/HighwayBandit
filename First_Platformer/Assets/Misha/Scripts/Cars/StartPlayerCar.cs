using UnityEngine;

public class StartPlayerCar : MonoBehaviour
{
    [SerializeField] private float _timeToStart = 7f;
    [SerializeField] private float _distanceToStartGame;
    
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
    }
}
