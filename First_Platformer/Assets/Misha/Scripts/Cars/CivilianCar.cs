using UnityEngine;

public class CivilianCar : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _distanceToDecreaseSpeed;
    [SerializeField] private float _changeSpeedValue;
    [SerializeField] private float _playerDistance;

    public float Speed { get { return _speed; } }

    private CarSpawner _carSpawner;
    private Transform _player;
    private Vector3 _forwardRay;

    private float _halfLengthOfCar;
    private float _halfHeightOfCar;

    private int _myStrip;

    public void Init(CarSpawner carSpawner, Transform player, int strip, float speed, float halfLength, float halfHeight)
    {
        _carSpawner = carSpawner;
        _player = player;
        _myStrip = strip;
        _speed = speed;
        _halfHeightOfCar = halfHeight;
        _halfLengthOfCar = halfLength;
    }

    private void FixedUpdate()
    {
        DoMove();
        CheckFrontCrash();
        //CheckDistanceToPlayer();
    }

    private void CheckDistanceToPlayer()
    {
        float distance = Vector3.Distance(transform.position, _player.position);

        if (distance < _playerDistance) return;

        _carSpawner.SpawnCarInFrontOfThePlayer(this, _myStrip, _halfLengthOfCar);
    }

    private void CheckFrontCrash()
    {
        _forwardRay = transform.position + new Vector3(0f, _halfHeightOfCar, _halfLengthOfCar);
        if (Physics.Raycast(_forwardRay, Vector3.forward, out RaycastHit hitInfo, _distanceToDecreaseSpeed)) 

        {
            if(hitInfo.collider.gameObject.GetComponentInParent<CivilianCar>() != null)
            {
                _speed -= _changeSpeedValue;
                CivilianCar forwardCar = hitInfo.collider.gameObject.GetComponentInParent<CivilianCar>();
                forwardCar.CollisionChangeSpeed(_changeSpeedValue);
            }
        }
    }

    public void CollisionChangeSpeed(float value)
    {
        _speed += value;
    }
    public void RespawnChangeSpeed(float value)
    {
        _speed = value;
    }

    private void DoMove()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 endPoint = new Vector3(_forwardRay.x, _forwardRay.y, _forwardRay.z + _halfLengthOfCar);
        Gizmos.DrawLine(_forwardRay, endPoint);
        Gizmos.DrawSphere(endPoint, 0.1f);
    }
}
