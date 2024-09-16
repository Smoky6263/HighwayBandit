using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _carSpawnPosition = new List<Transform>();
    [SerializeField] private List<GameObject> _carPrefab = new List<GameObject>();
    [SerializeField] private int _carsCount;
    [SerializeField] private float _carsSpeed;
    [SerializeField, Range(-2f, 2f)] private float _randomizeCarsSpeed;
    [SerializeField, Range(0f,4f)] private float _minDistance;
    [SerializeField, Range(2f,10f)] private float _maxDistance;

    [SerializeField] private Transform _player;

    private List<Transform> _carList;

    private void Awake()
    {
        _carList = new List<Transform>();
        SpawnCars();
    }


    private void SpawnCars()
    {
        int randomizeCar;
        float speed;
        float randomizeDistanceBetweenCars;
        float halfLength;
        float halfHeight;

        Vector3 currentSpawnPosition;
        Vector3 nextSpawnPosition;


        for (int strip = 0; strip < _carSpawnPosition.Count; strip++)
        {
            int carPerStrip = _carsCount / (int)_carSpawnPosition.Count;
            currentSpawnPosition = _carSpawnPosition[strip].localPosition;
            nextSpawnPosition = Vector3.zero;

            for (int i = 0; i < carPerStrip; i++)
            {
                CreateNewCar(out randomizeCar, out speed, out randomizeDistanceBetweenCars, out halfLength, out halfHeight);
                Transform car = Instantiate(_carPrefab[randomizeCar].transform, currentSpawnPosition + nextSpawnPosition, Quaternion.identity, _carSpawnPosition[strip]);
                CivilianCar civilianCar = car.GetComponent<CivilianCar>();
                civilianCar.Init(this, _player, strip, speed, halfLength, halfHeight);
                currentSpawnPosition = new Vector3(_carSpawnPosition[strip].position.x, _carSpawnPosition[strip].position.y, car.position.z + halfLength);
                nextSpawnPosition = new Vector3(0f,0f, halfLength + randomizeDistanceBetweenCars);
                _carList.Add(car);
                car.transform.name = $"Car {i}";
            }
        }
    }

    private void CreateNewCar(out int randomizeCar, out float speed, out float randomizeDistanceBetweenCars, out float halfLength, out float halfHeight)
    {
        randomizeCar = Random.Range(0, _carPrefab.Count);
        randomizeDistanceBetweenCars = Random.Range(_minDistance, _maxDistance);
        speed = _carsSpeed + Random.Range(-_randomizeCarsSpeed, _randomizeCarsSpeed);
        halfLength = _carPrefab[randomizeCar].transform.GetComponentInChildren<Renderer>().bounds.size.z / 2;
        halfHeight = _carPrefab[randomizeCar].transform.GetComponentInChildren<Renderer>().bounds.size.y / 2;
    }

    public void SpawnCarInFrontOfThePlayer(CivilianCar car, int strip, float halfLength)
    {
        float randomizeDistanceBetweenCars = Random.Range(_minDistance, _maxDistance);
        float speed = _carsSpeed + Random.Range(-_randomizeCarsSpeed, _randomizeCarsSpeed);
        Vector3 targetPosition = new Vector3(0f, 0f, halfLength + randomizeDistanceBetweenCars);
        car.RespawnChangeSpeed(speed);
        car.transform.position = _carList.Last().position + targetPosition;

    }
}
