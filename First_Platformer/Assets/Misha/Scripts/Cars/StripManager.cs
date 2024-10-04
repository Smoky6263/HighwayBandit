using System;
using System.Collections;
using UnityEngine;

public class StripManager : MonoBehaviour
{
    [SerializeField] private float _carRespawnBackDistance;
    [SerializeField] private float _carRespawnCrashDistance;

    private Transform _player;
    private Vehicle[] _carsArray;
    private CarsSpawner _carsSpawner;

    private int _carsCount;
    private bool _gameOver = false;

    public void Init(CarsSpawner carsSpawner, Transform player, int carsCount)
    {
        _carsSpawner = carsSpawner;
        _player = player;
        _carsCount = carsCount;
        _carsArray = new Vehicle[_carsCount];
    }

    private void Start()
    {
        LevelFinisher.OnVictoryCarSpawnEvent += StopCheckingDistance;
        LevelFinisher.PlayerDefeatEvent += StopCheckingDistance;
    }


    public void AddCarInArray(int id, Vehicle car)
    {
        if (car.GetComponent<Vehicle>() != null)
            _carsArray[id] = car;

        if(id == 0)
            StartCoroutine(RespawnCarCoroutine(0));

    }

    public void CarOnCrash(int id) => StartCoroutine(RespawnCarCoroutine(id));

    private IEnumerator RespawnCarCoroutine(int id)
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            Vector3 carPosition = _carsArray[id].GetComponent<Transform>().position;
            float playerPosition = _player.position.z;
            bool z_distance = playerPosition - carPosition.z > _carRespawnBackDistance;
            bool y_distance = carPosition.y > _carRespawnCrashDistance || carPosition.y < -_carRespawnBackDistance;
            if (z_distance || y_distance)
            {
                RespawnCarInFront(id);
                yield break;
            }
        }
    }
    private void RespawnCarInFront(int id)
    {
        float speed;
        float randomizeDistanceBetweenCars;
        float newCarHalfLength;

        _carsSpawner.CreateNewCar(out newCarHalfLength, out speed, out randomizeDistanceBetweenCars);
        Transform firstCar = _carsArray[_carsArray.Length - 1].GetComponent<Transform>();
        float firstCarHalfLength = firstCar.GetComponent<Vehicle>().HalfLengthOfCar;
        Vector3 firstCarPosition = new Vector3(0f, 0f, firstCar.transform.localPosition.z + firstCarHalfLength);
        Vector3 offsetPosition = new Vector3(0f, 0f, newCarHalfLength + randomizeDistanceBetweenCars);
        Vector3 targetPosition = firstCarPosition + offsetPosition;

        _carsArray[id].transform.localPosition = targetPosition;
        _carsArray[id].ResetParams(speed);
        Vehicle lastElement = _carsArray[id];

        for (int i = id; i < _carsCount - 1; i++)
        {
            Transform current = transform.GetChild(i);
            current.SetSiblingIndex(i + 1);
            _carsArray[i] = _carsArray[i + 1];
        }
        _carsArray[_carsArray.Length - 1] = lastElement;

        StartCoroutine(RespawnCarCoroutine(0));
    }

    public Vector3 ReturnFirstCarPosition()
    {
        Vector3 firstCarPosition = _carsArray[_carsArray.Length - 1].transform.position;
        Vector3 offset = new Vector3(0f, 0f, _carsArray[_carsArray.Length - 1].HalfLengthOfCar);

        return firstCarPosition + offset;
    }

    public float ReturnFirstCarSpeed() { return _carsArray[_carsArray.Length - 1].Speed; }
    private void StopCheckingDistance()
    {
        StopAllCoroutines();
        LevelFinisher.OnVictoryCarSpawnEvent -= StopCheckingDistance;
        LevelFinisher.PlayerDefeatEvent -= StopCheckingDistance;
    }

    private void OnEnable()
    {
        LevelFinisher.OnVictoryCarSpawnEvent += StopCheckingDistance;
        LevelFinisher.PlayerDefeatEvent += StopCheckingDistance;
    }

    private void OnDisable() => StopCheckingDistance();
    private void OnDestroy() => StopCheckingDistance();
    private void OnApplicationQuit()=> StopCheckingDistance();
}
