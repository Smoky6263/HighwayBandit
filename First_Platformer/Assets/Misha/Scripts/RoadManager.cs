using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{

    [SerializeField] private float _roadSpeed;
    [SerializeField] private int _samplesCount;
    [SerializeField] private float _sampleLength = 36f;
    [SerializeField] private List<GameObject> _roadSamples;

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
        for (int i = 0; i < _samplesCount; i++)
        {
            Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, _sampleLength * i);
            GameObject road = Instantiate(roadSamples[Random.Range(0, roadSamples.Count)], transform.position + targetPosition, Quaternion.identity, transform) ;
            RespawnRoad initRoad = road.GetComponent<RespawnRoad>();
            MovableObject movableObject = road.GetComponent<MovableObject>();
            movableObject.Init(_roadSpeed);
            initRoad.Init(i, _sampleLength, _samplesCount, _roadSpeed);
            _roadSamplesList.Add(road);
        }
    }
}
