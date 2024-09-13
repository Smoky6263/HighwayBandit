using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    [SerializeField] private List<Transform> _carSpawnPosition = new List<Transform>();
    [SerializeField] private List<GameObject> _carPrefab = new List<GameObject>();
    [SerializeField] private int _carsCount;
    [SerializeField, Range(0f,4f)] private float _minDistance;
    [SerializeField, Range(2f,10f)] private float _maxDistance;

    private List<Transform> _carsList = new List<Transform>();

    private float _distanceBetweenCars;
    private float _currentSpawnPosition = 0f;
    private float _halfLengthOfCurrentCar = 0f;

    private void Awake()
    {
        StartCoroutine(SpawnCarsCoroutine());
    }


    private IEnumerator SpawnCarsCoroutine()
    {
        for (int i = 0; i < _carSpawnPosition.Count; i++)
        {
            int carPerStrip = _carsCount / (int)_carSpawnPosition.Count;

            for (int b = 0; b < carPerStrip; b++)
            {
                int randomizeCar = Random.Range(0, _carPrefab.Count);
                float halfLength = _carPrefab[randomizeCar].transform.GetComponentInChildren<Renderer>().bounds.size.z / 2;
                Vector3 targetPosition = new Vector3(_carSpawnPosition[i].localPosition.x, _carSpawnPosition[i].localPosition.y, _carSpawnPosition[i].localPosition.z + _currentSpawnPosition + halfLength);
                Transform car = Instantiate(_carPrefab[randomizeCar].transform, targetPosition, Quaternion.identity, transform);
                _halfLengthOfCurrentCar = halfLength;
                _distanceBetweenCars = Random.Range(_minDistance, _maxDistance);
                _currentSpawnPosition = car.localPosition.z + _halfLengthOfCurrentCar + _distanceBetweenCars;

                _carsList.Add(car);
            }
            _currentSpawnPosition = 0f;
        }

        yield break;
    }
}
