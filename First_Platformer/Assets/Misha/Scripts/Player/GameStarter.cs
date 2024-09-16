using UnityEngine;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab, _startPlayerCarPrefab, _enemyCarPrefab;
    [SerializeField] private Transform _startPlayerCarSpawnPos, _enemyCarSpawnPos;
    
    [SerializeField] private float _spawnYoffset;

    private GameObject _player, _startPlayerCar, _enemyCar;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _startPlayerCar = Instantiate(_startPlayerCarPrefab, _startPlayerCarSpawnPos.transform.position, Quaternion.identity);
        _enemyCar = Instantiate(_enemyCarPrefab, _enemyCarSpawnPos.transform.position, Quaternion.identity);
        Vector3 spawnYoffset = new Vector3(0f, _spawnYoffset, 0f);
        _player = Instantiate(_playerPrefab, _startPlayerCarSpawnPos.transform.position + spawnYoffset, Quaternion.identity);
        _player.transform.SetParent(_startPlayerCar.transform);
        _enemyCar.GetComponent<EnemyCar>().Init(_player.transform);
    }
}
