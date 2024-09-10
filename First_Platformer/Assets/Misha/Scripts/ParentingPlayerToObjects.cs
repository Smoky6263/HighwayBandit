using UnityEngine;

public class ParentingPlayerToObjects : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {        
        transform.SetParent(collision.transform);
    }
}
