using UnityEngine;

public class StartPlayerCar : MonoBehaviour, ICar
{
    [SerializeField] private float _timeToStart = 7f;
    [SerializeField] private float _distanceToStartGame;

    private Vector3 _size;
    private Vector3 _boxCastPosition;

    private float _speed;
    private bool _gameStarted = false;

    public float Speed { get { return _speed; } }
    private void Awake()
    {
        _speed = _distanceToStartGame / _timeToStart;
    }

    private void FixedUpdate()
    {
        if (!_gameStarted)
            DoMove();
    }

    private void DoMove()
    {
        transform.Translate(transform.forward * _speed * Time.deltaTime);
    }

    public void ChangeSpeed(float value)
    {
        _speed = value;
    }
}
