using UnityEngine;

public class PlayerDownForce : MonoBehaviour
{
    private Rigidbody _rigidBody;

    private void Awake() => _rigidBody = GetComponent<Rigidbody>();
    private void FixedUpdate() => _rigidBody.AddForce(Vector3.down * 9.91f);
}
