using UnityEngine;

public class DelorianCar : MonoBehaviour
{
    private float _speed;
    private float _halfLength;
    private float _halfHeight;

    Vector3 _rayPosition;

    public void Init(float speed, float halfLength, float halfHeight)
    {
        _speed = speed;
        _halfLength = halfLength;
        _halfHeight = halfHeight;
    }

    private void FixedUpdate() => ChangeSpeed();

    private void ChangeSpeed()
    {
        _rayPosition = transform.position + new Vector3(0f, _halfHeight, -_halfLength);

        if(Physics.Raycast(_rayPosition, -transform.forward, out RaycastHit hitInfo, Mathf.Infinity))
        {
            if (hitInfo.transform.GetComponent<CivilianCar>() != null)
                _speed = hitInfo.transform.GetComponent<CivilianCar>().Speed;
        }

        transform.Translate(transform.forward * _speed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawRay(_rayPosition, -transform.forward);
    }
}
