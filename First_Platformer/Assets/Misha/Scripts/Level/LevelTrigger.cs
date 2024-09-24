using Supercyan.FreeSample;
using System;
using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    public event Action OnLevelTriggerEvent;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponentInChildren<SimpleSampleCharacterControl>() != null)
        {
            Debug.Log("Player Reach Teleport Trigger!");
            Transform parent = GetComponentInParent<Transform>();
            OnLevelTriggerEvent?.Invoke();
        }
    }
}
