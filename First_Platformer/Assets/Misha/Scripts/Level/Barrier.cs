using UnityEngine;

public class Barrier : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerOnJump>() != null)
        {
            other.GetComponent<PlayerOnJump>().CancelOnJumpCoroutine();
            other.transform.SetParent(null);
            Camera.main.transform.SetParent(null);
        }
    }
}
