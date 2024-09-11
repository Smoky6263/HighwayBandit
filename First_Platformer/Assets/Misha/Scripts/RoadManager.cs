using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public static RoadManager Instance { get; private set; }

    [SerializeField] private float _roadSpeed;
    [SerializeField] private int _roadCount;
    [SerializeField] private float _positionOffset = -36.0f;
    [SerializeField] private float _respawnPosition;
    [SerializeField] private List<GameObject> _roadSamples;
    public float RoadSpeed { get { return _roadSpeed; } private set { _roadSpeed = value; } }

    private List<GameObject> _roadSamplesList = new List<GameObject>();


    private void Start()
    {
        Init();
    }

    private void Init()
    {
        SpawnRoads(_roadSamples);
    }

    private void SpawnRoads(List<GameObject> roadSamples)
    {
        Vector3 positionOffset = new Vector3(0f, 0f, _positionOffset);
        for (int i = 0; i < _roadCount; i++)
        {
            GameObject road = Instantiate(roadSamples[Random.Range(0, roadSamples.Count)], transform.position + (positionOffset * i), Quaternion.identity, transform);
            RespawnRoad initRoad = road.GetComponent<RespawnRoad>();
            MovableObject movableObject = road.GetComponent<MovableObject>();
            movableObject.Init(_roadSpeed);
            initRoad.Init(_respawnPosition);
            _roadSamplesList.Add(road);
        }
    }
}
