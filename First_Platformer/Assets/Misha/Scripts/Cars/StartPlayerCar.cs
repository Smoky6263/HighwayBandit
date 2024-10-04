using UnityEngine;

public class StartPlayerCar : Vehicle
{
    [SerializeField] private float _timeToStart = 7f;
    [SerializeField] private float _distanceToStartGame;
    [SerializeField] private float _upCrashForce;

    private Rigidbody _rigidbody;
    private CrashMe _crashMe;

    private float _speed;
    private bool _gameStarted = false;
    private bool _inGame = true;

    public override float Speed { get { return _speed; } }
    public override bool InGame { get { return _inGame; } set { _inGame = value; } }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _crashMe = GetComponent<CrashMe>();
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

    public override void ChangeSpeed(float value)
    {
        _speed = value;
    }
    public override void OnCrash()
    {
        _rigidbody.isKinematic = false;
        _inGame = false;
        _speed = 0f;
        _crashMe.DoCrash(_upCrashForce);
        _gameStarted = true;
        Destroy(gameObject, 10f);
    }

    public override void Init(int id, float maxSpeed, float speed, float halfLength, float halfHeight)
    {

    }
}
