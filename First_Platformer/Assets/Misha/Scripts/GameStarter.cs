using Supercyan.FreeSample;
using System;
using System.Collections;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    [SerializeField, Range(0, 500)] private int _coinsCountToWin;
    [SerializeField] private GameObject _playerPrefab, _startPlayerCarPrefab;
    [SerializeField] private Transform _startPlayerCarSpawnPos, _enemyCarSpawnPos;
    
    [SerializeField] private float _spawnYoffset;

    private GameObject _player, _startPlayerCar;

    private CarsSpawner _carSpawner;
    private RoadManager _roadManager;

    private void Awake()
    {
        PlayerWallet.CoinsCount = 0;
        PlayerWallet.CoinsCountToEndLevel = _coinsCountToWin;
        Physics.gravity = Vector3.up * 0.1f;
        InitializeMainTransform();
        Init();
    }


    private void Init()
    {
        _startPlayerCar = Instantiate(_startPlayerCarPrefab, _startPlayerCarSpawnPos.transform.position, Quaternion.identity);
        Vector3 playerSpawnYoffset = new Vector3(0f, _spawnYoffset, 0f);
        _player = Instantiate(_playerPrefab, _startPlayerCarSpawnPos.transform.position + playerSpawnYoffset, Quaternion.identity);
        _player.transform.SetParent(_startPlayerCar.transform);
        StartCoroutine(StartGame());

        _carSpawner.Init(_player.transform);
        _roadManager.Init(_player.transform);
    }

    private IEnumerator StartGame()
    {
        _player.GetComponent<PlayerController>().enabled = false;

        yield return new WaitForSeconds(5f);

        _player.GetComponent<PlayerController>().enabled = true;
        yield break;
    }

    private void InitializeMainTransform()
    {
        _carSpawner = GetComponentInChildren<CarsSpawner>();
        _roadManager = GetComponentInChildren<RoadManager>();
    }
}
