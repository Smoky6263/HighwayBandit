using UnityEngine;

public class CivilianCar : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _distanceToDecreaseSpeed;
    [SerializeField] private float _changeSpeedValue;
    [SerializeField] private float _playerDistance;

    public int ID { get { return _id; } }
    public float Speed { get { return _speed; } }
    public float HalfLengthOfCar { get { return _halfLengthOfCar; } }


    private Vector3 _forwardRay;

    private int _id;

    private float _halfLengthOfCar;
    private float _halfHeightOfCar;


    public void Init(int id, float speed, float halfLength, float halfHeight)
    {
        _id = id;
        _speed = speed;
        _halfHeightOfCar = halfHeight;
        _halfLengthOfCar = halfLength;
    }

    private void FixedUpdate()
    {
        DoMove();
        CheckFrontCrash();
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
