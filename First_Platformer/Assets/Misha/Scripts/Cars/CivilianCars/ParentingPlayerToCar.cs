using Supercyan.FreeSample;
using UnityEngine;

public class ParentingPlayerToCar : MonoBehaviour
{
    private void Awake()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        SetParent(collision);
    }

    private void SetParent(Collision collision)
    {
        if (collision.transform.GetComponent<SimpleSampleCharacterControl>() != null)
        {
            collision.transform.SetParent(transform);
            collision.transform.GetComponent<SetUpCamera>().Camera.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        ResetParent(collision);
    }

    private void ResetParent(Collision collision)
    {
        if (collision.transform.GetComponent<SimpleSampleCharacterControl>() != null)
        {
            collision.transform.SetParent(null);
            collision.transform.GetComponent<SetUpCamera>().Camera.transform.SetParent(null);
        }
    }
}
