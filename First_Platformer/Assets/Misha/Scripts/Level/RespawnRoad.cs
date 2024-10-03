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

    private bool _itsFirstRespawn = true;

    private SpawnRandomProps _randomProps;

    public void Init(int id, float sampleLength, int samplesCount, float speed)
    {
        _randomProps = GetComponentInChildren<SpawnRandomProps>();
        _id = id;
        _speed = speed;
        _sampleLength = sampleLength;
        _samplesCount = samplesCount;
        _respawnArray = new float[samplesCount];
        _respawnArray = SetFirstRespawnDistance();
        _timeToRespawn = SetFirstRespawnTime();
    }

    private float[] SetFirstRespawnDistance()
    {
        for (int i = 0; i < _samplesCount; i++)
        {
            _respawnArray[i] = _sampleLength * i;
        }

        return _respawnArray;
    }

    private float SetFirstRespawnTime()
    {
        switch (_speed)
        {
            case > 0f:
                Array.Reverse(_respawnArray);
                _respawnDistance = _sampleLength + _respawnArray[_id];
                break;

            case < 0f:
                _respawnDistance = _sampleLength + _respawnArray[_id];
                break;

            default:
                break;
        }

        return _timeToRespawn = MathF.Abs(_respawnDistance / _speed);
    }

    private void Update()
    {
        RespawnPosition();
    }

    private void RespawnPosition()
    {
        if (_speed == 0f) return;

        _time += Time.deltaTime;

        if (_time < _timeToRespawn) return;

        _time = 0f;

        _randomProps.ResetElements();

        switch (_speed)
        {
            case < 0:
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.x, _respawnArray[_samplesCount - 1]);
                break;

            case > 0f:
                transform.localPosition = Vector3.zero;
                break;

            default:
                break;
        }

        if (_itsFirstRespawn == false) return;

        _respawnDistance = _sampleLength * _samplesCount;
        _timeToRespawn = MathF.Abs(_respawnDistance / _speed);
        _itsFirstRespawn = false;
    }
}