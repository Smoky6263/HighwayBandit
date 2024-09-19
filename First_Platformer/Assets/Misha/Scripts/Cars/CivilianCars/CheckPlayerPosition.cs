using System.Collections;
using UnityEngine;

public class CheckPlayerPosition : MonoBehaviour
{
    private StripManager _stripManager;
    private CivilianCar _civilianCar;
    private Transform _playerPosition;

    private float _teleportDistance;

    private int _id;
    private bool _firstCar = false;


    public void Init(StripManager stripManager, float distance)
    {
        _civilianCar = GetComponent<CivilianCar>();
        _id = _civilianCar.ID;
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
            float distance = Vector3.Distance(transform.position, _playerPosition.position);
            if (distance > _teleportDistance)
            {
                _stripManager.RespawnCar(_civilianCar);
                _playerPosition = null;
                _firstCar = false;
                Debug.Log(_id + "Я телепоритровался!");
                yield break;
            }
            yield return new WaitForFixedUpdate();
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
