using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    [Header("Интерфейс, когда игрок проиграл")]
    [SerializeField] private GameObject _gameOverPanel;

    [Header("Данные об уровне")]
    [SerializeField] private List<GameObject> _roadSamples;
    [SerializeField] private float _roadSpeed;
    [SerializeField] private int _samplesCount;
    [SerializeField] private float _sampleLength = 36f;

    [SerializeField] private int _forwardSpawnOffset;
    [SerializeField] private GameObject _teleportTrigger;

    [Header("Препятсвие для игрока")]
    [SerializeField] private GameObject _BarrierPrefab;
    [SerializeField] private Transform _barrierSpawner;
    [SerializeField] private float _barrierOffset;


    private List<GameObject> _roadSamplesList = new List<GameObject>();
    private LevelForwardScript _forwardScript;
    private LevelTrigger _levelTrigger;
    private Transform _player;

    public void Init(Transform player)
    {
        Mathf.Clamp(_forwardSpawnOffset, 0, _samplesCount);
        SpawnRoads(_roadSamples);
        SpawnBarrier();
        _levelTrigger = SpawnTeleportTrigger(_teleportTrigger);

        _forwardScript = GetComponent<LevelForwardScript>();
        _forwardScript.Init(_levelTrigger, _sampleLength);
    }

    

    private void SpawnRoads(List<GameObject> roadSamples)
    {
        for (int id = 0; id < _samplesCount; id++)
        {
            Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, _sampleLength * id);
            GameObject road = Instantiate(roadSamples[Random.Range(0, roadSamples.Count)], transform.position + targetPosition, Quaternion.identity, transform) ;
            RespawnRoad initRoad = road.GetComponent<RespawnRoad>();
            MovableObject movableObject = road.GetComponent<MovableObject>();
            PlayerOnGroundDefeat playerOnGroundDefeat = road.GetComponent<PlayerOnGroundDefeat>();

            movableObject.Init(_roadSpeed);
            initRoad.Init(id, _sampleLength, _samplesCount, _roadSpeed);
            playerOnGroundDefeat.Init(_gameOverPanel);
            _roadSamplesList.Add(road);
        }
    }

    private void SpawnBarrier()
    {
        Vector3 spawnerTargetPosition = new Vector3(0f, 0f, _sampleLength * _samplesCount + _barrierOffset);
        _barrierSpawner.position += spawnerTargetPosition;
        GameObject barrier = Instantiate(_BarrierPrefab, _barrierSpawner.position, Quaternion.identity);
        barrier.GetComponent<Barrier>().Init(_player, _barrierSpawner, _sampleLength * _samplesCount + _barrierOffset, _roadSpeed);
    }

    private LevelTrigger SpawnTeleportTrigger(GameObject teleportTrigger)
    {
        Transform teleport = Instantiate(teleportTrigger.transform, transform.position, Quaternion.identity, transform);
        LevelTrigger levelTrigger = teleport.GetComponent<LevelTrigger>();

        teleport.transform.localScale = new Vector3(36f, 20f, 36f);
        Vector3 targetPosition = new Vector3(0f, 2.026779f + teleport.transform.localScale.y / 2, transform.position.z + _sampleLength * _forwardSpawnOffset);
        teleport.localPosition = targetPosition;
        return levelTrigger;
    }
}
