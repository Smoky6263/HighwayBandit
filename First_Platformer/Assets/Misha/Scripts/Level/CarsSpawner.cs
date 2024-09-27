using System.Collections.Generic;
using UnityEngine;

public class CarsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _levelPassedPanel;

    [SerializeField] private List<Transform> _carSpawnPosition = new List<Transform>();
    [SerializeField] private List<GameObject> _carPrefabs = new List<GameObject>();
    [SerializeField] private GameObject _delorianPrefab;
    [SerializeField] private int _carsCount;
    [SerializeField] private float _carsSpeed;
    [SerializeField, Range(-2f, 2f)] private float _randomizeCarsSpeed;
    [SerializeField, Range(0f, 4f)] private float _minDistance;
    [SerializeField, Range(4f, 20f)] private float _maxDistance;


    private Transform _player;

    public void Init(Transform player)
    {
        LevelFinisher.OnVictoryCarSpawnEvent += SpawnDelorian;
        _player = player;
        SpawnCars();
    }

    private void SpawnDelorian()
    {
        float halfLength = _delorianPrefab.GetComponentInChildren<Renderer>().bounds.size.z / 2;
        float halfHeight = _delorianPrefab.GetComponentInChildren<Renderer>().bounds.size.y / 2;
        float randomizeDistanceBetweenCars = Random.Range(_minDistance, _maxDistance);
        int randomStrip = Random.Range(0, _carSpawnPosition.Count);
        float firstCarSpeed = _carSpawnPosition[randomStrip].GetComponent<StripManager>().ReturnFirstCarSpeed();
        Vector3 firstCarPosition = _carSpawnPosition[randomStrip].GetComponent<StripManager>().ReturnFirstCarPosition();
        Vector3 offsetPosition = new Vector3(0f ,0f, halfLength + 4f);
        GameObject delorian = Instantiate(_delorianPrefab, firstCarPosition + offsetPosition, Quaternion.identity);
        DelorianCar civilianDelorian = delorian.GetComponent<DelorianCar>();
        PlayerReachVictoryCar levelPassed = delorian.GetComponentInChildren<PlayerReachVictoryCar>();
        levelPassed.Init(_levelPassedPanel);
        civilianDelorian.Init(firstCarSpeed, halfLength, halfHeight);
        Debug.Log($"Level Passed, i spawn {delorian.name}");
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
            int carPerStrip = _carsCount / _carSpawnPosition.Count;
            currentSpawnPosition = _carSpawnPosition[strip].localPosition;
            nextSpawnPosition = Vector3.zero;
            
            StripManager stripManager = _carSpawnPosition[strip].GetComponent<StripManager>();
            stripManager.Init(this, _player, carPerStrip);

            for (int id = 0; id < carPerStrip; id++)
            {
                CreateNewCar(out randomizeCar, out speed, out randomizeDistanceBetweenCars, out halfLength, out halfHeight);
                GameObject car = Instantiate(_carPrefabs[randomizeCar], currentSpawnPosition + nextSpawnPosition, Quaternion.identity, _carSpawnPosition[strip]);
                
                CivilianCar civilianCar = car.GetComponent<CivilianCar>();
                civilianCar.Init(id, _carsSpeed, speed, halfLength, halfHeight);

                stripManager.AddCarInArray(id, car.transform);

                currentSpawnPosition = new Vector3(_carSpawnPosition[strip].position.x, _carSpawnPosition[strip].position.y, car.transform.position.z + halfLength);
                nextSpawnPosition = new Vector3(0f,0f, halfLength + randomizeDistanceBetweenCars);
                
                car.transform.name = $"Car {id}";
            }
        }
    }

    private void CreateNewCar(out int randomizeCar, out float speed, out float randomizeDistanceBetweenCars, out float halfLength, out float halfHeight)
    {
        randomizeCar = Random.Range(0, _carPrefabs.Count);
        randomizeDistanceBetweenCars = Random.Range(_minDistance, _maxDistance);
        speed = _carsSpeed + Random.Range(-_randomizeCarsSpeed, _randomizeCarsSpeed);
        halfLength = _carPrefabs[randomizeCar].GetComponentInChildren<Renderer>().bounds.size.z / 2;
        halfHeight = _carPrefabs[randomizeCar].GetComponentInChildren<Renderer>().bounds.size.y / 2;
    }
    public void CreateNewCar(out float halfLength,  out float speed, out float randomizeDistanceBetweenCars)
    {
        int randomizeCar = Random.Range(0, _carPrefabs.Count);
        randomizeDistanceBetweenCars = Random.Range(_minDistance, _maxDistance);
        halfLength = _carPrefabs[randomizeCar].GetComponentInChildren<Renderer>().bounds.size.z / 2;
        speed = _carsSpeed + Random.Range(-_randomizeCarsSpeed, _randomizeCarsSpeed);
    }

    private void OnDestroy() => LevelFinisher.OnVictoryCarSpawnEvent -= SpawnDelorian;
    private void OnDisable() => LevelFinisher.OnVictoryCarSpawnEvent -= SpawnDelorian;
    private void OnApplicationQuit() => LevelFinisher.OnVictoryCarSpawnEvent -= SpawnDelorian;
}
