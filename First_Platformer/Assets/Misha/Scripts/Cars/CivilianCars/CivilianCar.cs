using System.Collections;
using UnityEngine;

public class CivilianCar : MonoBehaviour
{
    [SerializeField] private GameObject _rearLightLeft;
    [SerializeField] private GameObject _rearLightRight;

    [SerializeField] private float _speed;
    [SerializeField] private float _distanceToDecreaseSpeed;
    [SerializeField] private float _distanceToIncreaseSpeed;
    [SerializeField] private float _increaseSpeed;
    [SerializeField] private float _decreaseSpeed;

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
        CheckFrontDistance();
        _speed = Mathf.Clamp(_speed, 1.5f, 4.5f);
        DoMove(_speed);
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

    private void DoMove(float speed)
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 endPoint = new Vector3(_forwardRay.x, _forwardRay.y, _forwardRay.z + _halfLengthOfCar);
        Gizmos.DrawLine(_forwardRay, endPoint);
        Gizmos.DrawSphere(endPoint, 0.1f);
    }
}