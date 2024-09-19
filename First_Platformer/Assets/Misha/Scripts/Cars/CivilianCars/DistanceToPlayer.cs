using System.Collections;
using UnityEngine;

public class DistanceToPlayer : MonoBehaviour
{
    private StripManager _stripManager;
    private CivilianCar _civilianCar;
    private Transform _playerPosition;

    private float _teleportDistance;

    private bool _firstCar = false;


    public void Init(StripManager stripManager, float distance)
    {
        _civilianCar = GetComponent<CivilianCar>();
        _stripManager = stripManager;
        _teleportDistance = distance;
    }

    public void SetFirstCar(Transform player)
    {
        _firstCar = true;
        _playerPosition = player;

        StartCoroutine(CheckPlayerDistanceCoroutine());
    }

    private IEnumerator CheckPlayerDistanceCoroutine()
    {
        while(_firstCar) 
        {
            yield return new WaitForFixedUpdate();
            float playerPosition = _playerPosition.position.z;
            float carPosition = transform.position.z;
            float distance = playerPosition - carPosition;
            if (distance > _teleportDistance)
            {
                _playerPosition = null;
                _firstCar = false;
                _stripManager.RespawnCar(_civilianCar);
                yield break;
            }
        }
    }

    private void OnDisable()
    {
        StopCoroutine(CheckPlayerDistanceCoroutine());
    }

    private void OnDestroy()
    {
        StopCoroutine(CheckPlayerDistanceCoroutine());

    }
    private void OnApplicationQuit()
    {
        StopCoroutine(CheckPlayerDistanceCoroutine());
    }
}
