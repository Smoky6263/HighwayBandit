using UnityEngine;

public class LevelForwardScript : MonoBehaviour
{
    [SerializeField] private int _stepsCount;

    private LevelTrigger _onLevelTrigger;

    private float _stepLength;

    public void Init(LevelTrigger levelTrigger, float stepLength)
    {
        _onLevelTrigger = levelTrigger;
        _onLevelTrigger.OnLevelTrigger += MoveLevel;
        _stepLength = stepLength;
    }

    private void MoveLevel()
    {
        Debug.Log("Action Work!");
        Vector3 targetPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + _stepLength * _stepsCount);
        transform.localPosition = targetPosition;
    }
}
