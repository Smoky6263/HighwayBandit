using Supercyan.FreeSample;
using System.Collections;
using UnityEngine;

public class CivilianCar : Vehicle
{
    [SerializeField] private GameObject _rearLightLeft;
    [SerializeField] private GameObject _rearLightRight;

    [SerializeField] private float _speed;
    [SerializeField] private float _maxSpeedOffset;
    [SerializeField] private float _distanceToDecreaseSpeed;
    [SerializeField] private float _distanceToIncreaseSpeed;
    [SerializeField] private float _increaseSpeed;
    [SerializeField] private float _decreaseSpeed;
    [SerializeField] private float _upCrashForce;

    private Rigidbody _rigidbody;
    private CrashMe _crashMe;

    private StripManager _stipManager;
    private CoinController _coinController;
    private Transform _playerPosition;

    private bool _lastCar = false;
    private bool _carCrashed = false;

    public override bool InGame { get { return _inGame; } set { _inGame = value; } }
    public override bool LastCar { get{ return _lastCar; } set { _lastCar = value; } }
    public override bool CarCrashed {get { return _carCrashed; } set { _carCrashed = value; } }

    public Transform PlayerPosition { get { return _playerPosition; } }
    public override int ID { get { return _id; } }
    public override float HalfLengthOfCar { get { return _halfLengthOfCar; } }
    public override float Speed { get { return _speed; } }


    private Vector3 _forwardRay;
    private int _id;

    private float _maxSpeed;
    private float _halfLengthOfCar;
    private float _halfHeightOfCar;

    private bool _inGame = true;

    public override void Init(int id, float maxSpeed, float speed, float halfLength, float halfHeight)
    {
        _stipManager = GetComponentInParent<StripManager>();
        _coinController = GetComponent<CoinController>();
        _rigidbody = GetComponent<Rigidbody>();
        _crashMe = GetComponent<CrashMe>();
        _id = id;
        _maxSpeed = maxSpeed;
        _speed = speed;
        _halfHeightOfCar = halfHeight;
        _halfLengthOfCar = halfLength;
    }

    private void FixedUpdate()
    {
        if (_inGame)
        {
            CheckFrontDistance();
            DoMove();
        }
    }

    private void CheckFrontDistance()
    {
        _forwardRay = transform.position + new Vector3(0f, _halfHeightOfCar, _halfLengthOfCar);

        if (Physics.Raycast(_forwardRay, Vector3.forward, out RaycastHit hitInfo, Mathf.Infinity))
        {
            if (hitInfo.collider.gameObject.GetComponentInParent<CivilianCar>() != null)
            {
                float distance = (hitInfo.transform.position.z - _halfLengthOfCar) - _forwardRay.z;

                if(distance < _distanceToDecreaseSpeed)
                {
                    _rearLightLeft.SetActive(true);
                    _rearLightRight.SetActive(true);
                    _speed -= _decreaseSpeed * Time.deltaTime;
                    CivilianCar forwardCar = hitInfo.collider.gameObject.GetComponentInParent<CivilianCar>();
                    forwardCar.ChangeSpeed(_increaseSpeed);
                }
                else 
                {
                    _rearLightLeft.SetActive(false);
                    _rearLightRight.SetActive(false);
                }
                if (distance > _distanceToIncreaseSpeed)
                {
                    _speed += _increaseSpeed * Time.deltaTime;
                }
            }
        }
    }

    public override void ChangeSpeed(float value)
    {
        _speed += value * Time.deltaTime;
    }

    private void DoMove()
    {
        _speed = Mathf.Clamp(_speed, 0f, _maxSpeed);
        transform.Translate(transform.forward *  _speed * Time.deltaTime);
    }

    public override void OnCrash()
    {
        if(!_carCrashed)
            _stipManager.CarOnCrash(transform.GetSiblingIndex());

        _rigidbody.isKinematic = false;
        InGame = false;
        _carCrashed = true;
        _speed = 0f;
        _coinController.CoinOnCarCrash(transform.up);
        _crashMe.DoCrash(_upCrashForce);
    }

    public override void ResetParams(float speed)
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);
        _coinController.RespawnCoin();
        _rigidbody.isKinematic = true;
        _speed = speed;
        _carCrashed = false;
        _inGame = true;
        _maxSpeed = 2f;
    }
}