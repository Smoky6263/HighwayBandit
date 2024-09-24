using UnityEngine;

public class CrashMe : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private float _upForce;

    public void Init(float upForce)
    {
        _upForce = upForce;
        _rigidbody = gameObject.AddComponent<Rigidbody>();
        _rigidbody.AddForce(new Vector3(Random.Range(-1f, 1f), 1f, Random.Range(-1f, 0f)) * _upForce, ForceMode.VelocityChange);
        _rigidbody.AddTorque(new Vector3(Random.Range(-3f, 3f), 0f, Random.Range(-3f, 3f)), ForceMode.VelocityChange);
    }
}
