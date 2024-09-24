using UnityEngine;

public class CivilianCar : MonoBehaviour, ICar
{
    [SerializeField] private GameObject _rearLightLeft;
    [SerializeField] private GameObject _rearLightRight;

    [SerializeField] private float _speed;
    [SerializeField] private float _maxSpeedOffset;
    [SerializeField] private float _distanceToDecreaseSpeed;
    [SerializeField] private float _distanceToIncreaseSpeed;
    [SerializeField] private float _increaseSpeed;
    [SerializeField] private float _decreaseSpeed;



    public int ID { get { return _id; } }
    public float Speed { get { return _speed; } set { _speed = value; } }
    public float HalfLengthOfCar { get { return _halfLengthOfCar; } }
    public bool InGame { get { return _inGame; } set { _inGame = value; } }


    private Vector3 _forwardRay;
    private int _id;

    private float _maxSpeed;
    private float _halfLengthOfCar;
    private float _halfHeightOfCar;

    private bool _inGame = true;

    public void Init(int id, float maxSpeed, float speed, float halfLength, float halfHeight)
    {
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

    public void ChangeSpeed(float value)
    {
        _speed += value * Time.deltaTime;
    }

    public void InitializeSpeed(float value)
    {
        _speed = value;
    }

    private void DoMove()
    {
        _speed = Mathf.Clamp(_speed, 0f, _maxSpeed);
        transform.Translate(transform.forward *  _speed * Time.deltaTime);
    }
}