using UnityEngine;

public class RespawnRoad : MonoBehaviour
{
    private float _respawnPosition;

    public void Init(float respawnPosition)
    {
        _respawnPosition = respawnPosition;
    }

    private void FixedUpdate()
    {
        RespawnRoadPosition();
    }

    private void RespawnRoadPosition()
    {
        if(transform.localPosition.z <= _respawnPosition)
        {
            transform.localPosition = Vector3.zero;
        }
    }
}
