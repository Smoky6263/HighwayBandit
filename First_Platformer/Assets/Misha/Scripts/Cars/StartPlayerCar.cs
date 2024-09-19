using Supercyan.FreeSample;
using System;
using UnityEngine;

public class StartPlayerCar : MonoBehaviour
{
    [SerializeField] private float _timeToStart = 7f;
    [SerializeField] private float _distanceToStartGame;
    [SerializeField] private float _decreaseSpeed;
    
    private Rigidbody _rigidbody;
    private float _speed;
    private bool _gameStarted = false;
    private void Awake()
    {
        _speed = _distanceToStartGame / _timeToStart;
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;
        _rigidbody.isKinematic = true;
    }

    private void FixedUpdate()
    {
        if (!_gameStarted)
            MoveCar();
    }


    private void MoveCar()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        StartGameMethod(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.GetComponent<SimpleSampleCharacterControl>() != null)
        {
            collision.transform.SetParent(null);
            collision.transform.GetComponent<SetUpCamera>().Camera.transform.SetParent(null);
        }
    }

    private void StartGameMethod(Collision collision)
    {
        if (collision.transform.GetComponent<CivilianCar>() != null)
        {
            _gameStarted = true;
            CrashCar();
        }
    }

    private void CrashCar()
    {
        _rigidbody.useGravity = true;
        _rigidbody.isKinematic = false;
    }
}
