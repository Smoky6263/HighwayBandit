using Supercyan.FreeSample;
using System;
using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    public event Action OnLevelTrigger;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponentInChildren<SimpleSampleCharacterControl>() != null)
        {
            Debug.Log("Player Reach Teleport Trigger!");
            Transform parent = GetComponentInParent<Transform>();
            OnLevelTrigger?.Invoke();
        }
    }
}
