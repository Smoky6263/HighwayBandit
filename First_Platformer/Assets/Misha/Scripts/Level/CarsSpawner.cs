using System.Collections.Generic;
using UnityEngine;

public class CarsSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _carSpawnPosition = new List<Transform>();
    [SerializeField] private List<GameObject> _carPrefab = new List<GameObject>();
    [SerializeField] private int _carsCount;
    [SerializeField] private float _carsSpeed;
    [SerializeField, Range(-2f, 2f)] private float _randomizeCarsSpeed;
    [SerializeField, Range(0f,4f)] private float _minDistance;
    [SerializeField, Range(2f,10f)] private float _maxDistance;


    private Transform _player;

    public List<GameObject> CarPrefab { get { return _carPrefab; } }


    public void Init(Transform player)
    {
        _player = player;
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
            int carPerStrip = _carsCount / _carSpawnPosition.Count;
            currentSpawnPosition = _carSpawnPosition[strip].localPosition;
            nextSpawnPosition = Vector3.zero;
            
            StripManager stripManager = _carSpawnPosition[strip].GetComponent<StripManager>();
            stripManager.Init(this, _player, carPerStrip);

            for (int id = 0; id < carPerStrip; id++)
            {
                CreateNewCar(out randomizeCar, out speed, out randomizeDistanceBetweenCars, out halfLength, out halfHeight);
                Transform car = Instantiate(_carPrefab[randomizeCar].transform, currentSpawnPosition + nextSpawnPosition, Quaternion.identity, _carSpawnPosition[strip]);
                
                CivilianCar civilianCar = car.GetComponent<CivilianCar>();
                civilianCar.Init(id, speed, halfLength, halfHeight);

                stripManager.AddCarInArray(id, car);

                currentSpawnPosition = new Vector3(_carSpawnPosition[strip].position.x, _carSpawnPosition[strip].position.y, car.position.z + halfLength);
                nextSpawnPosition = new Vector3(0f,0f, halfLength + randomizeDistanceBetweenCars);
                
                car.transform.name = $"Car {id}";
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
    public void CreateNewCar(out float halfLength,  out float speed, out float randomizeDistanceBetweenCars)
    {
        int randomizeCar = Random.Range(0, _carPrefab.Count);
        randomizeDistanceBetweenCars = Random.Range(_minDistance, _maxDistance);
        halfLength = _carPrefab[randomizeCar].transform.GetComponentInChildren<Renderer>().bounds.size.z / 2;
        speed = _carsSpeed + Random.Range(-_randomizeCarsSpeed, _randomizeCarsSpeed);
    }
}
