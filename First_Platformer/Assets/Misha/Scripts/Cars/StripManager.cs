using System;
using UnityEngine;

public class StripManager : MonoBehaviour
{
    [SerializeField] private float _carRespawnBackDistance;
    [SerializeField] private float _carRespawnFrontDistance;

    private Transform _player;
    private CarsSpawner _carsSpawner;
    private DistanceToPlayer[] _carsArray;
    private int _carsCount;
    public void Init(CarsSpawner carsSpawner, Transform player, int carsCount)
    {
        _carsSpawner = carsSpawner;
        _player = player;
        _carsCount = carsCount;
        _carsArray = new DistanceToPlayer[_carsCount];
    }

    public void AddCarInArray(int id, Transform car)
    {
        if (car.GetComponent<DistanceToPlayer>() != null)
        {
            _carsArray[id] = car.GetComponent<DistanceToPlayer>();
            _carsArray[id].Init(this, _player, _carRespawnBackDistance, _carRespawnFrontDistance);

            if (id == 0)
                _carsArray[id].SetLastCar();

            if (id == _carsArray.Length - 1)
                _carsArray[id].SetFirstCar();

        }
    }

    public void RespawnCarBackToFront(CivilianCar lastCar)
    {
        if (lastCar.GetComponent<CrashMe>() != null)
            Destroy(lastCar.GetComponent<CrashMe>());

        if (lastCar.GetComponent<Rigidbody>() != null)
            Destroy(lastCar.GetComponent<Rigidbody>());

        float speed;
        float randomizeDistanceBetweenCars;
        float newCarHalfLength;

        _carsSpawner.CreateNewCar(out newCarHalfLength, out speed, out randomizeDistanceBetweenCars);
        Transform firstCar = _carsArray[_carsArray.Length - 1].GetComponent<Transform>();
        float firstCarHalfLength = firstCar.GetComponent<CivilianCar>().HalfLengthOfCar;
        Vector3 firstCarPosition = new Vector3(0f, 0f, firstCar.transform.localPosition.z) + new Vector3(0f, 0f, firstCarHalfLength);
        Vector3 offsetPosition = new Vector3(0f, 0f, newCarHalfLength + randomizeDistanceBetweenCars);
        Vector3 targetPosition = firstCarPosition + offsetPosition;

        lastCar.transform.localPosition = targetPosition;
        lastCar.transform.rotation = Quaternion.Euler(Vector3.zero);
        lastCar.InGame = true;
        lastCar.Speed = speed;
        lastCar.GetComponent<CoinController>().RespawnCoin();
        DistanceToPlayer lastElement = _carsArray[0];

        for (int i = 0; i < _carsCount - 1; i++)
        {
            _carsArray[i] = _carsArray[i + 1];
        }
        _carsArray[_carsArray.Length - 1] = lastElement;
        
        _carsArray[0].SetLastCar();
        _carsArray[_carsArray.Length - 2].StopCheckPlayerInBackCoroutine();
        _carsArray[_carsArray.Length - 2].FirstCar = false;
        _carsArray[_carsArray.Length - 1].SetFirstCar();
    }

    public void RespawnCarFrontToBack(CivilianCar firstCar)
    {
        if (firstCar.GetComponent<CrashMe>() != null)
        {
            Destroy(firstCar.GetComponent<CrashMe>());
        }

        if (firstCar.GetComponent<Rigidbody>() != null)
        {
            Destroy(firstCar.GetComponent<Rigidbody>());
        }

        float speed;
        float randomizeDistanceBetweenCars;
        float newCarHalfLength;

        _carsSpawner.CreateNewCar(out newCarHalfLength, out speed, out randomizeDistanceBetweenCars);
        Transform lastCar = _carsArray[0].GetComponent<Transform>();
        float lastCarHalfLength = lastCar.GetComponent<CivilianCar>().HalfLengthOfCar;
        Vector3 lastCarPosition = new Vector3(0f, 0f, lastCar.transform.localPosition.z) + new Vector3(0f, 0f, -lastCarHalfLength);
        Vector3 offsetPosition = new Vector3(0f, 0f, newCarHalfLength + randomizeDistanceBetweenCars);
        Vector3 targetPosition = lastCarPosition - offsetPosition;

        firstCar.transform.localPosition = targetPosition;
        firstCar.InGame = true;
        firstCar.Speed = speed;
        lastCar.GetComponent<CoinController>().RespawnCoin();

        DistanceToPlayer firstElement = _carsArray[_carsArray.Length - 1];

        for (int i = _carsArray.Length - 1; i > 0; i--)
        {
            _carsArray[i] = _carsArray[i - 1];
        }
        _carsArray[0] = firstElement;
        _carsArray[0].SetLastCar();

        _carsArray[1].StopCheckPlayerInFrontCoroutine();
        _carsArray[1].LastCar = false;

        _carsArray[_carsArray.Length - 1].SetFirstCar();
    }
    
    public Vector3 ReturnFirstCarPosition()
    {
        Vector3 firstCarPosition = _carsArray[_carsArray.Length - 1].transform.position;
        Vector3 offset = new Vector3(0f, 0f, _carsArray[_carsArray.Length - 1].GetComponent<CivilianCar>().HalfLengthOfCar);

        return firstCarPosition + offset;
    }

    internal float ReturnFirstCarSpeed()
    {
        return _carsArray[_carsArray.Length - 1].GetComponent<CivilianCar>().Speed;
    }
}

