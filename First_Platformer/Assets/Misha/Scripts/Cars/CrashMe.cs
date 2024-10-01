using UnityEngine;

public class CrashMe : MonoBehaviour
{
    public void DoCrash(float upForce)
    {
        Rigidbody _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.AddForce(new Vector3(Random.Range(-1f, 1f), 1f, Random.Range(-1f, 0f)) * upForce, ForceMode.VelocityChange);
        _rigidbody.AddTorque(new Vector3(Random.Range(-3f, 3f), 0f, Random.Range(-3f, 3f)), ForceMode.VelocityChange);
    }
}
