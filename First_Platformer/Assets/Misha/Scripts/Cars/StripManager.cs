using System.Collections.Generic;
using UnityEngine;

public class StripManager : MonoBehaviour
{
    [SerializeField] private float _carRespawnDistance;

    private Transform _player;
    private CarsSpawner _carsSpawner;
    private CheckPlayerPosition[] _carsArray;
    private int _carsCount;
    public void Init(CarsSpawner carsSpawner,Transform player, int carsCount)
    {
        _carsSpawner = carsSpawner;
        _player = player;
        _carsCount = carsCount;
        _carsArray = new CheckPlayerPosition[_carsCount];

    }

    public void AddCarInArray(int id, Transform car)
    {
        _carsArray[id] = car.GetComponent<CheckPlayerPosition>();
        _carsArray[id].Init(this, _carRespawnDistance);

        if (id != 0)
            return;

        _carsArray[id].SetFirstCar( _player);

    }

    public void RespawnCar(CivilianCar lastCar)
    {
        float speed;
        float randomizeDistanceBetweenCars;
        float newCarHalfLength;

        _carsSpawner.CreateNewCar(out newCarHalfLength, out speed, out randomizeDistanceBetweenCars);
        Transform firstCar = _carsArray[_carsCount - 1].GetComponent<Transform>();
        float firstCarHalfLength = firstCar.GetComponent<CivilianCar>().HalfLengthOfCar;
        Vector3 firstCarPosition = firstCar.transform.localPosition + new Vector3(0f ,0f , firstCarHalfLength);
        Vector3 offsetPosition = new Vector3(0f, 0f, newCarHalfLength + randomizeDistanceBetweenCars);
        Vector3 targetPosition = firstCarPosition + offsetPosition;

        lastCar.transform.localPosition = targetPosition;
        CheckPlayerPosition lastElement = _carsArray[0];

        for (int i = 0; i < _carsCount - 1; i++)
        {
            _carsArray[i] = _carsArray[i + 1];
        }
        _carsArray[_carsCount - 1] = lastElement;
        _carsArray[0].SetFirstCar(_player);
    }
}
