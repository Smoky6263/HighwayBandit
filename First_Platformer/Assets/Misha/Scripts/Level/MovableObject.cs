using UnityEngine;

public class MovableObject : MonoBehaviour
{
    private float _speed;    

    public virtual void Init(float speed)
    {
        _speed = speed;
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }
}
