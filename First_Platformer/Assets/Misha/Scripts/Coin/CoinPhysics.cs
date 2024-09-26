using System.Collections;
using UnityEngine;

public class CoinPhysics : MonoBehaviour
{
    [SerializeField] private float _upForce;
    [SerializeField] private float _horizontalForce;

    private Rigidbody _rigidbody;
    private MeshCollider _meshCollider;

    public void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _meshCollider = GetComponent<MeshCollider>();

        _meshCollider.enabled = false;
        _rigidbody.useGravity = false;
        _rigidbody.isKinematic = true;
    }

    private void StartAddForceCoroutine(Vector3 direction) => StartCoroutine(AddForceCoroutine(direction));
    private void StopAddForceCoroutine() => StopCoroutine("AddForceCoroutine");

    private IEnumerator AddForceCoroutine(Vector3 direction)
    {
        while (_rigidbody.isKinematic == false)
        {
            _rigidbody.AddForce(direction.x * _horizontalForce, 1f * _upForce, direction.z * _horizontalForce, ForceMode.VelocityChange);
            _rigidbody.AddTorque(new Vector3(Random.Range(-3f, 3f), 0f, Random.Range(-3f, 3f)), ForceMode.VelocityChange);

            if (_rigidbody.isKinematic == false)
                yield break; ;
        }
    }

    public void TurnOnPhysics(Vector3 direction)
    {
        _meshCollider.enabled = false;
        _rigidbody.useGravity = true;
        _rigidbody.isKinematic = false;
        StartAddForceCoroutine(direction);
    }
    public void TurnOffPhysics()
    {
        _rigidbody.useGravity = false;
        _rigidbody.isKinematic = true;
        StopAddForceCoroutine();
        transform.localPosition = Vector3.zero;
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    private void OnDisable()
    {
        StopCoroutine("AddForceCoroutine");
    }

    private void OnDestroy()
    {
        StopCoroutine("AddForceCoroutine");
    }

    private void OnApplicationQuit()
    {
        StopCoroutine("AddForceCoroutine");
    }
}
