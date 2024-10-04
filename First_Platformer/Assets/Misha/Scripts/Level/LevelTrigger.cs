using Supercyan.FreeSample;
using System;
using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    public event Action OnLevelTriggerEvent;
    private void OnTriggerEnter(Collider other)
    {
        PlayerOnTrigger(other);
    }
    private void PlayerOnTrigger(Collider other)
    {
        if (other.gameObject.GetComponentInChildren<PlayerController>() != null || other.GetComponent<Camera>() != null)
        {
            Debug.Log($"{other.gameObject.name} Reach Teleport Trigger!");
            Transform parent = GetComponentInParent<Transform>();
            OnLevelTriggerEvent?.Invoke();
        }
    }

}
