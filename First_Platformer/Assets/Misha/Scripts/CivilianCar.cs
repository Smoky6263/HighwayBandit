using UnityEngine;

public class CivilianCar : MonoBehaviour
{
    [SerializeField] private float _speed;

    public void Init(float speed)
    {
        _speed = speed;
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }
}
