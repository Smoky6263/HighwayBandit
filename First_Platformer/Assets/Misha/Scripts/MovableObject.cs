using UnityEngine;

public class MovableObject : MonoBehaviour
{
    private RoadManager _roadManager;
    private float _speed;

    private void Start()
    {
        Init();
    }    

    private void Init()
    {
        _roadManager = RoadManager.Instance;
        _speed = _roadManager.RoadSpeed;
    }

    private void FixedUpdate()
    {
        _speed = _roadManager.RoadSpeed;
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }
}
