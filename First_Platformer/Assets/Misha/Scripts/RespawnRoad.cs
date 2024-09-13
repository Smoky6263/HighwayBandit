using System;
using UnityEngine;

public class RespawnRoad : MonoBehaviour
{
    private int _id;
    private float _speed;
    private float _time;
    private float _timeToRespawn;

    private float[] _respawnArray;
    private float _respawnDistance;
    private float _sampleLength;
    private int _samplesCount;

    public void Init(int id, float sampleLength, int samplesCount, float speed)
    {
        _time = 0f;
        _id = id;
        _speed = speed;
        _sampleLength = sampleLength;
        _samplesCount = samplesCount;
        _respawnArray = new float[samplesCount];
        SetFirstRespawnDistance();
        SetFirstRespawnTime();
    }

    private void Update()
    {
        RespawnSamplePosition();
    }

    private void SetFirstRespawnDistance()
    {
        for (int i = 0; i < _samplesCount; i++)
        {
            _respawnArray[i] = _sampleLength * i;
        }
    }

    private void SetFirstRespawnTime()
    {
        if (_speed > 0f)
        {
            Array.Reverse(_respawnArray);
            _respawnDistance = _sampleLength + _respawnArray[_id];
        }
        else 
        {
            _respawnDistance = _sampleLength + _respawnArray[_id];
        }

        _timeToRespawn = MathF.Abs(_respawnDistance / _speed);
    }

    private void RespawnSamplePosition()
    {
        _time += Time.deltaTime;

        if (_time < _timeToRespawn)
            return;
        
        if (_speed > 0f)
            transform.localPosition = Vector3.zero;
        else
            transform.localPosition = new Vector3 (transform.localPosition.x, transform.localPosition.x, _respawnArray[_samplesCount - 1]);

        _respawnDistance = _sampleLength * _samplesCount;
        _timeToRespawn = MathF.Abs(_respawnDistance / _speed);
        _time = 0f;
    }
}
