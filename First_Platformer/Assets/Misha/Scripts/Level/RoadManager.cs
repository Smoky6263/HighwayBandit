using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverPanel;

    [SerializeField] private List<GameObject> _roadSamples;
    [SerializeField] private float _roadSpeed;
    [SerializeField] private int _samplesCount;
    [SerializeField] private float _sampleLength = 36f;

    [SerializeField] private int _forwardSpawnOffset;
    [SerializeField] private GameObject _teleportTrigger;


    private List<GameObject> _roadSamplesList = new List<GameObject>();
    private LevelForwardScript _forwardScript;
    private LevelTrigger _levelTrigger;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        Mathf.Clamp(_forwardSpawnOffset, 0, _samplesCount);
        SpawnRoads(_roadSamples);
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
