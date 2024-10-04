using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    private Collider _myCollider;

    private Vector3 _size;
    private Vector3 _halfSize;
    private Vector3 _boxCastPosition;

    private void Awake() 
    {
        _myCollider = GetComponent<Collider>();
        _size = GetComponent<Collider>().bounds.size;
        _halfSize = _size / 2;
    }

    private void FixedUpdate() => CollisionCheck();
    private void CollisionCheck()
    {
        _boxCastPosition = GetComponent<Collider>().bounds.center;
        Collider[] hitColliders = Physics.OverlapBox(_boxCastPosition, _halfSize, Quaternion.identity);

        foreach (Collider collider in hitColliders)
        {
            if (_myCollider != collider && collider.transform.GetComponent<Vehicle>() != null && transform.GetComponent<Vehicle>().InGame && collider.transform.GetComponent<Vehicle>().InGame)
            {
                transform.GetComponent<Vehicle>().OnCrash();
                collider.transform.GetComponent<Vehicle>().OnCrash();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(_boxCastPosition, _size);
    }
}
