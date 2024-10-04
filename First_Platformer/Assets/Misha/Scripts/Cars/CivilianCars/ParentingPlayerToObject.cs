using Supercyan.FreeSample;
using UnityEngine;

public class ParentingPlayerToObject : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<PlayerController>() != null)
            SetPlayerParent(collision);
    }

    

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.GetComponent<PlayerController>() != null)
            ResetPlayerParent(collision);
    }

    private void SetPlayerParent(Collision collision)
    {
        collision.transform.SetParent(transform);
        collision.transform.GetComponent<SetUpCamera>().Camera.transform.SetParent(transform);
    }    

    private void ResetPlayerParent(Collision collision)
    {
        collision.transform.SetParent(null);
        collision.transform.GetComponent<SetUpCamera>().Camera.transform.SetParent(null);
    }
}
