using System;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab, _startPlayerCarPrefab, _enemyCarPrefab;
    [SerializeField] private Transform _startPlayerCarSpawnPos, _enemyCarSpawnPos;
    
    [SerializeField] private float _spawnYoffset;

    private GameObject _player, _startPlayerCar, _enemyCar;

    private CarsSpawner _carSpawner;

    private void Awake()
    {
        InitializeMainTransform();
        Init();
    }


    private void Init()
    {
        _startPlayerCar = Instantiate(_startPlayerCarPrefab, _startPlayerCarSpawnPos.transform.position, Quaternion.identity);
        _enemyCar = Instantiate(_enemyCarPrefab, _enemyCarSpawnPos.transform.position, Quaternion.identity);

        Vector3 playerSpawnYoffset = new Vector3(0f, _spawnYoffset, 0f);
        _player = Instantiate(_playerPrefab, _startPlayerCarSpawnPos.transform.position + playerSpawnYoffset, Quaternion.identity);
        _player.transform.SetParent(_startPlayerCar.transform);

        _enemyCar.GetComponent<EnemyCar>().Init(_player.transform);
        _carSpawner.Init(_player.transform);
    }
    private void InitializeMainTransform()
    {
        foreach (Transform child in transform)
        {
            if(child.GetComponentInChildren<CarsSpawner>() != null)
                    _carSpawner = child.GetComponentInChildren<CarsSpawner>();
        }
    }
}
